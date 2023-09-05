using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] float _speedRotation = 2f;
    [SerializeField] float _speed = 3f;

    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] float _groundDistance = 0.1f;

    private void Update()
    {
        RotateCar();
        MoveCar();
    }

    void RotateCar()
    {
        float direction = Input.GetAxisRaw("Horizontal");
        _rigidbody.rotation = Quaternion.Euler(_rigidbody.rotation.eulerAngles.x, direction * (_speedRotation / 90) + _rigidbody.rotation.eulerAngles.y, _rigidbody.rotation.eulerAngles.z); 
    }

    void MoveCar()
    {

        if (Input.GetKey(KeyCode.Z) && CheckIfOnGround())
        {
            _rigidbody.velocity = _speed * transform.forward;
        }
    }

    bool CheckIfOnGround()
    {
         return Physics.Raycast(transform.position, -transform.up, _groundDistance);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * _groundDistance);
    }
}
