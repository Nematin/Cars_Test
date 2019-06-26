using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int CarNumber = 1;
    public GameObject PrefabCar;
    public Transform Finish;
    public Slider mutationRate;
    public Slider mutationAmplitude;
    public Text text_mutationRate;
    public Text text_mutationAmplitude;

    public List<CarController> currentPopulation;
    public List<List<CarController>> allGenerations;

    private List<CarController> newGeneration = new List<CarController>();
    private List<CarController> oldGeneration = new List<CarController>();

    private float _mutationRate;
    private float _mutationAmplitude;


    

    private GameObject[] countOnScreen;

    void InitPopulation()
    {
        for (int i = 0; i < CarNumber; i++)
        {
            var car = Instantiate(PrefabCar, new Vector3(-0f, 0f, 0), Quaternion.identity);
            car.GetComponent<CarController>().target = Finish.position;
            float scaleForLeftWheel = Random.Range(0.0f, 0.5f);
            float scaleForRightWheel = Random.Range(0.0f, 0.5f);


            var leftWheel = car.transform.GetChild(1);
            leftWheel.transform.localScale += scaleForLeftWheel * (new Vector3(1, 1, 1));

            var rightWheel = car.transform.GetChild(2);
            rightWheel.transform.localScale += scaleForRightWheel * (new Vector3(1, 1, 1));

            currentPopulation.Add(car.GetComponent<CarController>());
        }
    }

    void NextGeneration()
    {
        newGeneration = new List<CarController>();
        oldGeneration = new List<CarController>();

        int survivorCut = 5;
        List<CarController> survivors = new List<CarController>();

        for (int i = 0; i < survivorCut; i++)
        {
            survivors.Add(GetFittest());
        }

        for (int i = 0; i < currentPopulation.Count; i++)
        {
            Destroy(currentPopulation[i].gameObject);
        }

        currentPopulation.Clear();


        //for (int i = 0; i < survivors.Count; i++)
        //{
        //    var car = Instantiate(PrefabCar, new Vector3(-0f, 0f, 0), Quaternion.identity);
        //    car.GetComponent<CarController>().target = Finish.position;

        //    car.GetComponent<CarController>().BWSize = survivors[i].BWSize;
        //    car.GetComponent<CarController>().FWSize = survivors[i].FWSize;

        //    //currentPopulation.Add(survivors[i]);
        //}

        //for (int i = 0; i < survivors.Count; i++)
        //{
        //    for (int j = 0; j < survivors.Count; j++)
        //    {
        //        Crossover(survivors[i], survivors[j]);
        //    }
        //}

        while (currentPopulation.Count < CarNumber)
        {

            for (int i = 0; i < survivors.Count; i++)
            {
                for (int j = 0; j < survivors.Count; j++)
                {
                    CrossoverWithMutation(survivors[i], survivors[j], _mutationAmplitude);
                }

                if (currentPopulation.Count >= CarNumber)
                {
                    break;
                }
            }
        }

        for (int i = 0; i < survivors.Count; i++)
        {
            Destroy(survivors[i].gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitPopulation();
    }

    // Update is called once per frame
    void Update()
    {
        
        _mutationRate = mutationRate.value;
        text_mutationRate.text = _mutationRate.ToString();
        _mutationAmplitude = mutationAmplitude.value;
        text_mutationAmplitude.text = _mutationAmplitude.ToString();


        countOnScreen = GameObject.FindGameObjectsWithTag("Player");

        if (countOnScreen.Length == 0)
            NextGeneration();
    }

    CarController GetFittest()
    {
        float maxFitness = float.MinValue;
        int index = 0;
        for (int i = 0; i < currentPopulation.Count; i++)
        {
            if (currentPopulation[i].fitness > maxFitness)
            {
                maxFitness = currentPopulation[i].fitness;
                index = i;
            }
        }

        CarController fittest = currentPopulation[index];
        currentPopulation.Remove(fittest);
        return fittest;
    }

    void Crossover(CarController parent1, CarController parent2)
    {
        var car = Instantiate(PrefabCar, new Vector3(-0f, 0f, 0), Quaternion.identity);
        car.GetComponent<CarController>().target = Finish.position;

        float BWSize1 = parent1.transform.GetChild(1).localScale.x;
        float FWSize1 = parent1.transform.GetChild(2).localScale.x;

        float BWSize2 = parent2.transform.GetChild(1).localScale.x;
        float FWSize2 = parent2.transform.GetChild(2).localScale.x;

        float BWSize_new = (BWSize1 + BWSize2) / 2;
        float FWSize_new = (FWSize1 + FWSize2) / 2;

        car.GetComponent<CarController>().BWSize = BWSize_new;
        car.GetComponent<CarController>().FWSize = FWSize_new;

        var leftWheel = car.transform.GetChild(1);
        leftWheel.transform.localScale = car.GetComponent<CarController>().BWSize * (new Vector3(1, 1, 1));

        var rightWheel = car.transform.GetChild(2);
        rightWheel.transform.localScale = car.GetComponent<CarController>().FWSize * (new Vector3(1, 1, 1));

        currentPopulation.Add(car.GetComponent<CarController>());
    }

    void CrossoverWithMutation(CarController parent1, CarController parent2, float mutationAmplitude)
    {
        
        var car = Instantiate(PrefabCar, new Vector3(-0f, 0f, 0), Quaternion.identity);
        car.GetComponent<CarController>().target = Finish.position;

        float BWSize1 = parent1.transform.GetChild(1).localScale.x;
        float FWSize1 = parent1.transform.GetChild(2).localScale.x;

        float BWSize2 = parent2.transform.GetChild(1).localScale.x;
        float FWSize2 = parent2.transform.GetChild(2).localScale.x;

        float BWSize_new = (BWSize1 + BWSize2) / 2;
        float FWSize_new = (FWSize1 + FWSize2) / 2;

        car.GetComponent<CarController>().BWSize = (BWSize_new + (2 * Random.Range(0.0f, 2.0f) - 1) * BWSize_new * mutationAmplitude);
        car.GetComponent<CarController>().FWSize = (FWSize_new + (2 * Random.Range(0.0f, 2.0f) - 1) * FWSize_new * mutationAmplitude);

        var leftWheel = car.transform.GetChild(1);
        leftWheel.transform.localScale = car.GetComponent<CarController>().BWSize * (new Vector3(1, 1, 1));

        var rightWheel = car.transform.GetChild(2);
        rightWheel.transform.localScale = car.GetComponent<CarController>().FWSize * (new Vector3(1, 1, 1));

        currentPopulation.Add(car.GetComponent<CarController>());
    }

}
