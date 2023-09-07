using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollisionHandler : MonoBehaviour
{
    [SerializeField] ExplosionGauge _explosivityGauge;
    [SerializeField] int _damagePerImpact = 1;
    [SerializeField] float _cooldownImpact = 3f;

    [SerializeField] private Rigidbody _fridgeRb;
    [SerializeField] private float _strengthImpact;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" && !_explosivityGauge.IsInvincible)
        {
            StartCoroutine(CoroutineCoolDown());
            _explosivityGauge.TakeDamage(_damagePerImpact);
        }

        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb)
        {
            Vector3 pos = transform.position;
            Vector3 colPos = collision.transform.position;
            Vector3 dir = (pos - colPos).normalized + new Vector3(0,1,0);
            
            rb.AddForce(dir * (_fridgeRb.velocity.magnitude *_strengthImpact), ForceMode.Impulse);
        }
    }

    IEnumerator CoroutineCoolDown()
    {
        _explosivityGauge.IsInvincible = true;
        yield return new WaitForSeconds(_cooldownImpact);
        _explosivityGauge.IsInvincible = false;
    }
}
