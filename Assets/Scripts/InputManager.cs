using System;
using System.Collections.Generic;
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

[CreateAssetMenu(fileName = "Inputs", menuName = "AutoDestructionInputs/Add inputs")]
public class ListInputs : ScriptableObject
{
    [SerializeField] List<PairInputs> _listPairInputs;

    public List<PairInputs> ListPairInputs { get => _listPairInputs; }
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
    private float _lastMousePositionX;
    [SerializeField] private float _sensibility = 1f;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
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
        Vector2 currentMousePositionX = Input.mousePosition;

        float mouseDelta = currentMousePositionX.x - _lastMousePositionX;

        _lastMousePositionX = currentMousePositionX.x;
        return mouseDelta * _sensibility;
    } 
}
