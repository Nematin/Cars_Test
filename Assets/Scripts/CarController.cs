using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    public float CriticalSpeed = 0.05f;
    public float MaxIdleTime = 3;
    public float timerToLive = 60;
    public float FWSize;
    public  float BWSize;

    public float fit;

    private float timer = 0;
    private Vector3 lastPosition;
    public Vector2 target;

    public bool hasFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.GetChild(0).position;
        FWSize = transform.GetChild(2).localScale.x;
        BWSize = transform.GetChild(1).localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        var currentPosition = transform.GetChild(0).position;
        var velocity = (currentPosition.x - lastPosition.x) / Time.deltaTime;

        if (Mathf.Abs(currentPosition.x - lastPosition.x) >= 0.5f)
            timerToLive = 30;
        else
            timerToLive -= Time.deltaTime;

        if (velocity > CriticalSpeed)
            timer = 0;
        else
            timer += Time.deltaTime;

        //Debug.Log(velocity);

        lastPosition = currentPosition;

        if (timer > MaxIdleTime)
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }

        if ((timerToLive <= 0) || (hasFinished == true))
            gameObject.SetActive(false);

        if (currentPosition.x >= target.x)
            gameObject.SetActive(false);

        fit = fitness;


    }

    public float fitness
    {
        get
        {
            float dist = Vector2.Distance(transform.GetChild(0).position, target);
            if (dist == 0)
            {
                dist = 0.0001f;
            }
            return (60 / dist) * (Random.Range(0, 1) == 1 ? 0.75f : 1f);
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Flame")
            hasFinished = true;
    }
}
