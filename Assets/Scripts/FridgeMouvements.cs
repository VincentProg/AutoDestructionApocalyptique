using UnityEngine;

public class FridgeMouvements : MonoBehaviour
{
    [SerializeField] private Transform _car;
    private Rigidbody _rb;
    
    private bool _canJump;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _sideForce;
    
    [SerializeField] private PhysicMaterial _physicMat;
    [SerializeField] private WinLossConditions _winLossConditions;
    [SerializeField] private StartingNumbers _startingNumbers;
    bool _isStopped = true;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (_winLossConditions != null)
        {
            _winLossConditions.OnWinForklift += OnWin;
            _winLossConditions.OnWinFridge += OnWin;
        }
        if (_startingNumbers != null)
        {
            _startingNumbers.OnStartingEnded += OnCircuitStarted;
        }
    }

    private void OnCircuitStarted()
    {
        _isStopped = false;
    }

    void OnWin(bool fridgeWon)
    {
        _isStopped = true;
    }

    private void Update()
    {
        if (!_isStopped)
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
        
            SetFriction();
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
    
    private void SetFriction()
    {
        if(Input.GetKeyDown(InputManager.Instance.GetKeyCodeFromInput(MachineInput.ButtonDown)))
        {
            _physicMat.dynamicFriction = 0;
        } else if(Input.GetKeyUp(InputManager.Instance.GetKeyCodeFromInput(MachineInput.ButtonDown)))
        {
            _physicMat.dynamicFriction = 3;
        }
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
