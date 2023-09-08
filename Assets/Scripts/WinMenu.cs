using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    [SerializeField]
    private Button _restartButton, _menuButton;
    [SerializeField] private WinLossConditions _conditions;

    [SerializeField]
    private GameObject _menuObject;
    [SerializeField] bool _isWinFridgeMenu;
    private void Start()
    {
        _restartButton.onClick.AddListener(Restart);
        _menuButton.onClick.AddListener(ReturnToMenu);
        if (_isWinFridgeMenu)
        {
            _conditions.OnWinFridge += OnWin;
        } else
        {
            _conditions.OnWinForklift += OnWin;
        }
    }

    private void OnWin(bool fridgeWon)
    {
        _menuObject.SetActive(true);
    }

    private void OnDestroy()
    {
        _restartButton.onClick.RemoveListener(Restart);
        _menuButton.onClick.RemoveListener(ReturnToMenu);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
