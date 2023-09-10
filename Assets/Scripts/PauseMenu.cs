using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private KeyCode keyPause;
    [SerializeField] 
    private GameObject menuParent;
    [SerializeField] 
    private Button resumeButton, menuButton;

    private bool isPaused;

    private void Start()
    {
        resumeButton.onClick.AddListener(Resume);
        menuButton.onClick.AddListener(BackToMenu);
    }

    private void OnDestroy()
    {
        resumeButton.onClick.RemoveListener(Resume);
        menuButton.onClick.RemoveListener(BackToMenu);
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyPause) || Input.GetKeyDown(InputManager.Instance.GetKeyCodeFromInput(MachineInput.ButtonWhite)))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        isPaused = true;
        menuParent.SetActive(true);
        Time.timeScale = 0;
    }

    private void Resume()
    {
        isPaused = false;
        menuParent.SetActive(false);
        Time.timeScale = 1;
    }

    private void BackToMenu()
    {
        Resume();
        SceneManager.LoadScene(0);
    }
}
