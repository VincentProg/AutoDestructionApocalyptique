using System;
using System.Collections;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] float _speedRotation = 2f;
    [SerializeField] float _lowSpeed = 10f;
    [SerializeField] float _highSpeed = 5f;
    float _currentSpeed;
    [SerializeField] float _groundDistance = 0.1f;
    [SerializeField] float _loadingBoostDuration = 1f;
    [SerializeField] float _boostDuration = 2f;
    [SerializeField] float _boostSpeed = 15f;
    bool _hasLoadedSpeedBoost = false;
    bool _isSpeedBoostActive = false;
    bool _isAtHighSpeed = true;
    bool _isStopped = false;

    [SerializeField] Rigidbody _rigidbody;
    Coroutine _routineBoost;
    [SerializeField] WinLossConditions _winLossConditions;
    [SerializeField] Camera _camera;
    [SerializeField,Range(0f, 180f)] float _angleRotationMax = 45f;

    private void Awake()
    {
        _currentSpeed = _highSpeed;    
    }
    private void Start()
    {
        _winLossConditions.OnWinForklift += OnWin;
        _winLossConditions.OnWinFridge += OnWin;
    }

    private void Update()
    {
        if (!_isStopped)
        {
            CheckBoost();
            ApplyBoost();
            ChangeSpeed();
        }
    }
    
    private void FixedUpdate()
    {
        if (!_isStopped)
        {
            RotateCar();
            MoveCar();
        }
    }

    void RotateCar()
    {
        float direction = InputManager.Instance.GetWheelValue();

        Quaternion rightAngle = Quaternion.LookRotation(Quaternion.Euler(0f, _angleRotationMax, 0f) * _camera.transform.forward);
        Quaternion leftAngle = Quaternion.LookRotation(Quaternion.Euler(0f, -_angleRotationMax, 0f) * _camera.transform.forward);
        
        float valueClamped = direction * _speedRotation * Time.fixedDeltaTime + _rigidbody.rotation.eulerAngles.y;
        if (direction < 0f)
        {
            bool checkIfZero = IsZeroBetweenTwoValues(leftAngle.eulerAngles.y, rightAngle.eulerAngles.y);
            if (valueClamped < leftAngle.eulerAngles.y && ((checkIfZero && valueClamped > rightAngle.eulerAngles.y) || !checkIfZero))
            {
                valueClamped = leftAngle.eulerAngles.y;
            }
        }

        if (direction > 0f)
        {
            bool checkIfZero = IsZeroBetweenTwoValues(leftAngle.eulerAngles.y, rightAngle.eulerAngles.y);
            if (valueClamped > rightAngle.eulerAngles.y && ((checkIfZero && valueClamped < leftAngle.eulerAngles.y) || !checkIfZero))

            {
                valueClamped = rightAngle.eulerAngles.y;
            }
        }
        Quaternion rotation = Quaternion.Euler(_rigidbody.rotation.eulerAngles.x, valueClamped, _rigidbody.rotation.eulerAngles.z);
        _rigidbody.rotation = rotation; 
    }

    bool IsZeroBetweenTwoValues(float angleLeft,float angleRight)
    {
        return (angleLeft - 180) > 0f && (angleRight - 180) < 0f;
    }

    void MoveCar()
    {

        if (CheckIfOnGround())
        {
            _rigidbody.velocity = _currentSpeed * Time.fixedDeltaTime * transform.forward;
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
        Gizmos.color = Color.blue;

        float lengthLines = 10;
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0f, _angleRotationMax, 0f) * _camera.transform.forward * lengthLines);
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0f, - _angleRotationMax, 0f) * _camera.transform.forward * lengthLines);
    }

    void ChangeSpeed()
    {
        
        if (Input.GetKeyDown(InputManager.Instance.GetKeyCodeFromInput(MachineInput.LeverLeftUp)) && _currentSpeed != _highSpeed)
        {
            _isAtHighSpeed = true;
            if (!_isSpeedBoostActive)
            {
                _currentSpeed = _highSpeed;
            }
        }

        if (Input.GetKeyDown(InputManager.Instance.GetKeyCodeFromInput(MachineInput.LeverLeftDown)) && _currentSpeed != _lowSpeed)
        {
            _isAtHighSpeed = false;
            if (!_isSpeedBoostActive)
            {
                _currentSpeed = _lowSpeed;
            }
        }
    }

    float GetSpeedWithoutBoost()
    {
        return _isAtHighSpeed? _highSpeed:_lowSpeed;
    }
    void CheckBoost()
    {
        if (!_hasLoadedSpeedBoost && Input.GetKeyDown(InputManager.Instance.GetKeyCodeFromInput(MachineInput.LeverRightDown)))
        {
            _routineBoost = StartCoroutine(CoroutineBoostLoading());
        }
        if (_routineBoost != null && Input.GetKeyUp(InputManager.Instance.GetKeyCodeFromInput(MachineInput.LeverRightDown)))
        {
            StopCoroutine(_routineBoost);
            _routineBoost = null;
        }
    }

    void ApplyBoost()
    {
        if (_hasLoadedSpeedBoost && Input.GetKeyDown(InputManager.Instance.GetKeyCodeFromInput(MachineInput.LeverRightUp)))
        {
            _hasLoadedSpeedBoost = false;
            StartCoroutine(CoroutineApplyBoost());
        }
    }

    IEnumerator CoroutineBoostLoading()
    {
        yield return new WaitForSeconds(_loadingBoostDuration);
        _hasLoadedSpeedBoost = true;
        Debug.Log("BOOST LOADED");
    }
    IEnumerator CoroutineApplyBoost()
    {
        _isSpeedBoostActive = true;
        _currentSpeed = _boostSpeed;
        yield return new WaitForSeconds(_boostDuration);
        _currentSpeed = GetSpeedWithoutBoost();
        _isSpeedBoostActive = false;
    }

    void OnWin(bool fridgeWon)
    {
        _isStopped = true;
        _rigidbody.velocity = Vector3.zero;
    }
}
