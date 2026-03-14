using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] private Button StartButton;
    [SerializeField] private Button QuitGameButton;

    private void Awake()
    {
        StartButton.onClick.AddListener(() =>
        {
            StartGame();
        });
        QuitGameButton.onClick.AddListener(() =>
        {
            QuitGame();
        });
    }

    private void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
