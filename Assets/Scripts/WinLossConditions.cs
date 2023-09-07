using System;
using UnityEngine;

public class WinLossConditions : MonoBehaviour
{
    [SerializeField] ExplosionGauge _explosityGauge;

    public event Action<bool> OnWinFridge;
    public event Action<bool> OnWinForklift;
    private bool _gameEnded = false;
    

    private void Start()
    {
        _explosityGauge.OnValueChanged += OnGaugeValueChanged;
    }

    private void OnGaugeValueChanged(float gaugeValue)
    {
        if (_explosityGauge.IsGaugeFull() && !_gameEnded)
        {
            _gameEnded = true;
            OnWinFridge?.Invoke(true); // True : fridge has won
        }
    }
}
