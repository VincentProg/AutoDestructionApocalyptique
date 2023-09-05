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
    //[SerializeField] float _;

    [SerializeField] Rigidbody _rigidbody;

    private void Awake()
    {
        _currentSpeed = _highSpeed;    
    }

    private void Update()
    {
        ChangeSpeed();
        RotateCar();
        MoveCar();
    }

    void RotateCar()
    {
        float direction = Input.GetAxisRaw("Horizontal");
        _rigidbody.rotation = Quaternion.Euler(_rigidbody.rotation.eulerAngles.x, direction * (_speedRotation / 45) + _rigidbody.rotation.eulerAngles.y, _rigidbody.rotation.eulerAngles.z); 
    }

    void MoveCar()
    {

        if (CheckIfOnGround())
        {
            _rigidbody.velocity = _currentSpeed * transform.forward;
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
        if (Input.GetKeyDown(KeyCode.T) && _currentSpeed != _highSpeed)
        {
            _currentSpeed = _highSpeed;
        }

        if (Input.GetKeyDown(KeyCode.G) && _currentSpeed != _lowSpeed)
        {
            _currentSpeed = _lowSpeed;
        }
    }

    void CheckBoost()
    {
        //bool hasBoost = Input.GetKey(KeyCode.H);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //_rigidbody.velocity = Vector3.zero;
    }
}
