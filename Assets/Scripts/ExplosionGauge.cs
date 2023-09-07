using System;
using UnityEngine;
using UnityEngine.UI;

public class ExplosionGauge : MonoBehaviour
{
    [SerializeField] float _maxGaugeValue = 15f;
    [SerializeField] Slider _slider;
    float _gaugeValue = 0f;
    bool _isInvincible = false;
    [SerializeField] float _autoDamagePerSecond = 0.5f;

    public event Action<float> OnValueChanged;
    public bool IsInvincible { get => _isInvincible; set => _isInvincible = value; }

    public void TakeDamage(float damage)
    {
        SetGaugeValue(_gaugeValue + damage);
    }

    void SetGaugeValue(float value)
    {
        _gaugeValue = Mathf.Clamp(value, 0f, _maxGaugeValue);
        _slider.value = _gaugeValue /_maxGaugeValue;
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
