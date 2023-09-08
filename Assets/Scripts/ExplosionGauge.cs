using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ExplosionGauge : MonoBehaviour
{
    [SerializeField] float _maxGaugeValue = 15f;
    [SerializeField] Transform _pointerGauge;
    float _gaugeValue = 0f;
    bool _isInvincible = true;
    [SerializeField] float _autoDamagePerSecond = 0.5f;

    public event Action<float> OnValueChanged;
    public bool IsInvincible { get => _isInvincible; set => _isInvincible = value; }
    [SerializeField] private StartingNumbers _startingNumbers;
    private void Start()
    {
        if (_startingNumbers != null)
        {
            _startingNumbers.OnStartingEnded += OnCircuitStarted;
        }
    }

    private void OnCircuitStarted()
    {
        _isInvincible = false;
    }

    public void TakeDamage(float damage)
    {
        SetGaugeValue(_gaugeValue + damage);
    }

    void SetGaugeValue(float value)
    {
        _gaugeValue = Mathf.Clamp(value, 0f, _maxGaugeValue);
        if (_pointerGauge != null)
        {
            _pointerGauge.rotation = Quaternion.Euler(0f, 0f, ((_gaugeValue / _maxGaugeValue) * 180) - 90);
        }
        OnValueChanged?.Invoke(_gaugeValue);
    }

    private void Update()
    {
        if (!_isInvincible)
        {
            TakeDamage(_autoDamagePerSecond * Time.deltaTime);
        }
    }

    public bool IsGaugeFull()
    {
        return _gaugeValue == _maxGaugeValue;
    }
}
