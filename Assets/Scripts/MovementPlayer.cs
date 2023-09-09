using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
    bool _isStopped = true;

    [SerializeField] Rigidbody _rigidbody;
    Coroutine _routineBoost;
    [SerializeField] WinLossConditions _winLossConditions;
    [SerializeField] Camera _camera;
    [SerializeField,Range(0f, 180f)] float _angleRotationMax = 45f;
    [SerializeField] StartingNumbers _startingNumbers;

    [SerializeField]
    private List<Transform> _wheels;

    [SerializeField] private AudioSource _motorSource;
    [SerializeField] private float _boostPitch;
    private float _initialPitch;

    [SerializeField] private ParticleSystem _particlesBoost;
    [SerializeField] private ParticleSystem _particlesSpeed;

    private void Awake()
    {
        _initialPitch = _motorSource.pitch;
        _currentSpeed = 0f;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _winLossConditions.OnWinForklift += OnWin;
        _winLossConditions.OnWinFridge += OnWin;
        if (_startingNumbers != null)
        {
            _startingNumbers.OnStartingEnded += OnCircuitStarted;
        }
    }

    private void OnCircuitStarted()
    {
        _currentSpeed = _highSpeed;
        _isStopped = false;
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

        foreach (Transform wheel in _wheels)
        {
            wheel.transform.Rotate(new Vector3(0,0,1), _rigidbody.velocity.magnitude);
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
    }
    IEnumerator CoroutineApplyBoost()
    {
        _isSpeedBoostActive = true;
        _currentSpeed = _boostSpeed;
        _motorSource.pitch = _boostPitch;
        _particlesBoost.Play();
        _particlesSpeed.Play();
        yield return new WaitForSeconds(_boostDuration);
        _particlesSpeed.Stop();
        _particlesBoost.Stop();
        _motorSource.pitch = _initialPitch;
        _currentSpeed = GetSpeedWithoutBoost();
        _isSpeedBoostActive = false;
    }

    void OnWin(bool fridgeWon)
    {
        _isStopped = true;
        _rigidbody.velocity = Vector3.zero;
    }

}
