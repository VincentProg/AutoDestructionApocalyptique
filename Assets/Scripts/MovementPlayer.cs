using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] float _speedRotation = 2f;
    [SerializeField] float _speed = 3f;

    [SerializeField] Rigidbody _rigidbody;

    private void Update()
    {
        RotateCar();
        MoveCar();
    }

    void RotateCar()
    {
        float direction = Input.GetAxisRaw("Horizontal");
        _rigidbody.rotation = Quaternion.Euler(0, direction * (_speedRotation / 90) + _rigidbody.rotation.eulerAngles.y, 0); 
    }

    void MoveCar()
    {
        _rigidbody.velocity = _speed * transform.forward;
    }
}
