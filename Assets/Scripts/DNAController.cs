using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNAController
{
    public List<Vector2> genes = new List<Vector2>();

    public float[] Genes = new float[2];
    public List<float[]> populationGenes = new List<float[]>();

    public DNAController (int size)
    {
        for (int i = 0; i < size; i++)
        {
            genes.Add(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
        }
    }

    public DNAController(DNAController parent1, DNAController parent2)
    {
        Genes = new float[parent1.Genes.Length];
        for (int i = 0; i < Genes.Length; i++)
        {
            Genes[i] = (parent1.Genes[i] + parent2.Genes[i]) / 2;
        }
    }

    public void Mutate(float mutationAmplitude)
    {
        for (int i = 0; i < Genes.Length; i++)
        {
            Genes[i] = Genes[i] + (2 * Random.Range(0, 2) - 1) * Genes[i] * mutationAmplitude;
        }
    }
}
