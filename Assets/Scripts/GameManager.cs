using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Singleton pattern
    public static GameManager Instance { get; private set; }
    private InputActions inputActions;

    private Meteor meteor;

    //UI gameobjects
    [SerializeField] private TextMeshProUGUI scoreTextUI;
    [SerializeField] private TextMeshProUGUI finalScoreTextUI;

    [SerializeField] private Player player;


    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private GameObject PauseMenuUI;

    //Pause UI
    [SerializeField] private Button ContinueButton;
    [SerializeField] private Button QuitToMenuButton;

    //GameOver UI
    [SerializeField] private Button RetryButton;
    [SerializeField] private Button ReturntoMenuButton;

    private float score = 0f;

    //State Machine
    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }

    private GameState currentState;

    private void ChangeState(GameState newState)
    {
        currentState = newState;

        //reset all UI
        PauseMenuUI.SetActive(false);
        GameOverUI.SetActive(false);

        switch (newState)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                PauseMenuUI.SetActive(true);
                break;

            case GameState.GameOver:
                Time.timeScale = 0f;
                finalScoreTextUI.text = Mathf.FloorToInt(score).ToString();
                GameOverUI.SetActive(true);
                DestroyAllMeteors();
                break;
        }
    }



    private void Awake()
    {
        Instance = this;
        inputActions = new InputActions(); //Assign inputactions created
        PauseMenuUI.SetActive(false);
        GameOverUI.SetActive(false);
        score += Time.deltaTime * 0f;
        Time.timeScale = 0f;

        //UI listners

        ContinueButton.onClick.AddListener(() => { ChangeState(GameState.Playing); });
        QuitToMenuButton.onClick.AddListener(() => { ReturnToMenu(); });

        RetryButton.onClick.AddListener(() => { RetryLevel(); });
        ReturntoMenuButton.onClick.AddListener(() => { ReturnToMenu(); });
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
    }

    private void OnDisable()
    {
        inputActions.UI.Disable();
    }

    private void Update()
    {
        if (currentState == GameState.Playing)
        {
            score += Time.deltaTime;
            scoreTextUI.text = Mathf.FloorToInt(score).ToString();
        }

        if (currentState == GameState.Playing && inputActions.UI.MainMenu.WasPressedThisFrame())
        {
            TogglePause();
        }
    }

    private void Start()
    {
        ChangeState(GameState.Playing);
    }

    private void TogglePause()
    {
        if (currentState == GameState.Playing)
            ChangeState(GameState.Paused);
        else if (currentState == GameState.Paused)
            ChangeState(GameState.Playing);
    }

    public void TriggerGameOver()
    {
        ChangeState(GameState.GameOver);
    }

    private void RetryLevel()
    {
        score = 0f;
        SceneManager.LoadScene("GameScene");
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f; // reset before switching
        SceneManager.LoadScene("MainMenu");
    }

    //Destroy all active meteors
    private void DestroyAllMeteors()
    {
        foreach (Meteor meteor in Meteor.ActiveMeteors.ToArray())
        {
            Destroy(meteor.gameObject);
        }
    }
}
