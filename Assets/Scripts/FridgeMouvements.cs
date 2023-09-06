using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeMouvements : MonoBehaviour
{
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
        if (Input.GetKeyDown(InputManager.Instance.GetKeyCodeFromInput(MachineInput.ButtonUp)))
        {
            Jump();
        }

        if (Input.GetKey(InputManager.Instance.GetKeyCodeFromInput(MachineInput.ButtonLeft)))
        {
            AddSideForce(false);
        }

        if (Input.GetKey(InputManager.Instance.GetKeyCodeFromInput(MachineInput.ButtonRight)))
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
