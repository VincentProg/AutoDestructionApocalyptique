using System;
using UnityEngine;

public enum MachineInput
{
    Wheel,
    LeverLeftUp,
    LeverLeftDown,
    LeverRightUp,
    LeverRightDown,
    ButtonUp,
    ButtonDown,
    ButtonLeft,
    ButtonRight,
    ButtonWhite,
    ButtonBlack
}

[Serializable]
public class PairInputs {
    [SerializeField] MachineInput _input;
    [SerializeField] KeyCode _keyCode;

    public KeyCode KeyCode { get => _keyCode;}
    public MachineInput Input { get => _input;}
}


public class InputManager : MonoBehaviour
{

    private static InputManager _instance;

    [SerializeField] ListInputs _listInputs;
    public static InputManager Instance { get => _instance; }
    [SerializeField] private float _sensibility = 1f;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
        }
    }

    public KeyCode GetKeyCodeFromInput(MachineInput input)
    {
        bool found = false;
        KeyCode keyCode = KeyCode.None;
        int i = 0;
        while(!found && i < _listInputs.ListPairInputs.Count)
        {
            if (_listInputs.ListPairInputs[i].Input == input)
            {
                found = true;
                keyCode = _listInputs.ListPairInputs[i].KeyCode;
            }
            ++i;
        }
        return keyCode;
    }
    public float GetWheelValue()
    {
        return Input.GetAxis("Mouse X") * _sensibility;
    } 
}
