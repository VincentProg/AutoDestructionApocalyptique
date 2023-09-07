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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 size = new Vector3(_boxCollider.size.x * transform.localScale.x, _boxCollider.size.y * transform.localScale.y, _boxCollider.size.z * transform.localScale.z);
        Gizmos.DrawWireCube(transform.position + _boxCollider.center, size);
    }
}
