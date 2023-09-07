using UnityEngine;
using UnityEngine.UI;

public class CustomMenuNavigation : MonoBehaviour
{
    [SerializeField] Button[] _buttons;
    private int selectedIndex = 0;

    private void Start()
    {
        if (_buttons.Length > 0)
        {
            _buttons[selectedIndex].Select();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(InputManager.Instance.GetKeyCodeFromInput(MachineInput.ButtonUp)))
        {
            NavigateUp();
        }
        else if (Input.GetKeyDown(InputManager.Instance.GetKeyCodeFromInput(MachineInput.ButtonDown)))
        {
            NavigateDown();
        }
    }

    private void NavigateUp()
    {
        if (_buttons.Length > 0)
        {
            selectedIndex = (selectedIndex - 1 + _buttons.Length) % _buttons.Length;
            _buttons[selectedIndex].Select();
        }
    }

    private void NavigateDown()
    {
        if (_buttons.Length > 0)
        {
            selectedIndex = (selectedIndex + 1) % _buttons.Length;
            _buttons[selectedIndex].Select();
        }
    }
}