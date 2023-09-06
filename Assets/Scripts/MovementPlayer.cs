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

    [SerializeField] Rigidbody _rigidbody;
    Coroutine _routineBoost;

    private void Awake()
    {
        _currentSpeed = _highSpeed;    
    }

    private void Update()
    {
        CheckBoost();
        ApplyBoost();
        ChangeSpeed();
        RotateCar();
        MoveCar();
    }

    void RotateCar()
    {
        float direction = InputManager.Instance.GetWheelValue();
        _rigidbody.rotation = Quaternion.Euler(_rigidbody.rotation.eulerAngles.x, direction * _speedRotation * Time.deltaTime + _rigidbody.rotation.eulerAngles.y, _rigidbody.rotation.eulerAngles.z); 
    }

    void MoveCar()
    {

        if (CheckIfOnGround())
        {
            _rigidbody.velocity = _currentSpeed * Time.deltaTime * transform.forward;
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
}
