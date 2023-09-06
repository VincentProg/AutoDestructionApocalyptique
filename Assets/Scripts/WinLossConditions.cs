using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLossConditions : MonoBehaviour
{
    [SerializeField] ExplosivityGauge _explosityGauge;

    private void Start()
    {
        _explosityGauge.OnValueChanged += OnGaugeValueChanged;
    }

    private void OnGaugeValueChanged(float gaugeValue)
    {
        if (_explosityGauge.IsGaugeFull())
        {
            Debug.Log("LOSE");
        }
    }
}
