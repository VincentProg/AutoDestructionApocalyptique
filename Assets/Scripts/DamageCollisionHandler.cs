using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollisionHandler : MonoBehaviour
{
    [SerializeField] ExplosivityGauge _explosivityGauge;
    [SerializeField] int _damagePerImpact = 1;
    [SerializeField] float _cooldownImpact = 3f;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" && !_explosivityGauge.IsInvincible)
        {
            StartCoroutine(CoroutineCoolDown());
            _explosivityGauge.TakeDamage(_damagePerImpact);
        }
    }

    IEnumerator CoroutineCoolDown()
    {
        _explosivityGauge.IsInvincible = true;
        yield return new WaitForSeconds(_cooldownImpact);
        _explosivityGauge.IsInvincible = false;
    }
}
