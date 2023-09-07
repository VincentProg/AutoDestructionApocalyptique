using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] WinLossConditions _winLossConditions;
    [SerializeField] BoxCollider _boxCollider;
    public event Action OnFinishLineAchieved;
    private void OnTriggerEnter(Collider other)
    {
        OnFinishLineAchieved?.Invoke();
    }
}
