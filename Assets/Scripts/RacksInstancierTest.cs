using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RacksInstancierTest : MonoBehaviour
{
    [SerializeField] private List<Transform> racks;
    [SerializeField] private int nbrCubes;

    private void Start()
    {
        for (int i = 0; i < nbrCubes; i++)
        {
            int x = Random.Range(-500, 500) + (int)transform.position.x;
            int z = Random.Range(-500, 500) + (int)transform.position.z;
            
            Instantiate(racks[i%racks.Count], new Vector3(x, 3, z),
                Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f)));
        }
    }
    
}
