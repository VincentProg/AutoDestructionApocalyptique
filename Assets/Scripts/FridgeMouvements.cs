using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeMouvements : MonoBehaviour
{
    [SerializeField] private KeyCode _jump, _left, _right;
    [SerializeField] private Transform _car;
    private Rigidbody _rb;
    
    private bool _canJump;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _sideForce;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_jump))
        {
            Jump();
        }

        if (Input.GetKey(_left))
        {
            AddSideForce(false);
        }

        if (Input.GetKey(_right))
        {
            AddSideForce(true);
        }
    }

    private void Jump()
    {
        if(_canJump)
        {
            _rb.AddForce(new Vector3(0, _jumpForce), ForceMode.VelocityChange);
        }
    }

    private void AddSideForce(bool isRight)
    {
        Vector3 vec = _car.right;
        if(!isRight)
        {
            vec = -vec;
        }
        
        _rb.AddForce(vec * (_sideForce), ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            _canJump = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            _canJump = false;
        }
    }
}
