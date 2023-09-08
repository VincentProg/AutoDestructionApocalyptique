using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource _source1;
    [SerializeField]
    private AudioSource _source2;

    private void Start()
    {
        _source1.clip.LoadAudioData();
        _source2.clip.LoadAudioData();
        _source1.Play();
        StartCoroutine(Wait(0.4f));
    }
    
    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        _source2.Play();
    }
}
