using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button playButton, quitButton;

    private void Start()
    {
        playButton.onClick.AddListener(Play);
        quitButton.onClick.AddListener(Quit);
    }
    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(Play);
        quitButton.onClick.RemoveListener(Quit);
    }
    
    private void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
