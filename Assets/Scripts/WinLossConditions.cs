using System;
using UnityEngine;

public class WinLossConditions : MonoBehaviour
{
    [SerializeField] ExplosionGauge _explosityGauge;
    [SerializeField] FinishLine _finishLine;

    public event Action<bool> OnWinFridge;
    public event Action<bool> OnWinForklift;
    private bool _gameEnded = false;
    

    private void Start()
    {
        _explosityGauge.OnValueChanged += OnGaugeValueChanged;
        _finishLine.OnFinishLineAchieved += OnFinishLineAchieved;
    }

    private void OnFinishLineAchieved()
    {
        if (!_gameEnded)
        {
            _gameEnded = true;
            OnWinForklift?.Invoke(false);
        }
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
