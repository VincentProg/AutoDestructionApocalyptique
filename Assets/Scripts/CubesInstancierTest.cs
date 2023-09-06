using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubesInstancierTest : MonoBehaviour
{
    [SerializeField] private Transform cube;
    [SerializeField] private int nbrCubes;

    private void Start()
    {
        for (int i = 0; i < nbrCubes; i++)
        {
            int x = Random.Range(-500, 500);
            int z = Random.Range(-500, 500);
            Instantiate(cube, new Vector3(x, 3, z),
                Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f)));
        }
    }
    
}
