using UnityEngine;

[CreateAssetMenu(fileName = "Inputs", menuName = "AutoDestructionInputs/Add inputs")]
public class InputManager : ScriptableObject
{
    [Header("Joystick Left")]
    [SerializeField] KeyCode _keyCodeLeverLeftUp;
    [SerializeField] KeyCode _keyCodeLeverLeftDown;
    [Header("Joystick Right")]
    [SerializeField] KeyCode _keyCodeLeverRightUp;
    [SerializeField] KeyCode _keyCodeLeverRightDown;
    [Header("Buttons")]
    [SerializeField] KeyCode _keyCodeUp;
    [SerializeField] KeyCode _keyCodeLeft;
    [SerializeField] KeyCode _keyCodeRight;
    [SerializeField] KeyCode _keyCodeDown;
    [SerializeField] KeyCode _keyCodeWhiteButton;
    [SerializeField] KeyCode _keyCodeBlackButton;

    #region getters
    public KeyCode KeyCodeLeverLeftUp { get => _keyCodeLeverLeftUp;}
    public KeyCode KeyCodeLeverRightUp { get => _keyCodeLeverRightUp;}
    public KeyCode KeyCodeLeverLeftDown { get => _keyCodeLeverLeftDown; }
    public KeyCode KeyCodeLeverRightDown { get => _keyCodeLeverRightDown; }
    public KeyCode KeyCodeUp { get => _keyCodeUp;}
    public KeyCode KeyCodeLeft { get => _keyCodeLeft;}
    public KeyCode KeyCodeRight { get => _keyCodeRight;}
    public KeyCode KeyCodeDown { get => _keyCodeDown; }
    public KeyCode KeyCodeWhite { get => _keyCodeWhiteButton; }
    public KeyCode KeyCodeBlack { get => _keyCodeBlackButton; }
    #endregion
}
