using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    public static GamePauseUI Instance { get; private set; }

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionButton;

    private void Awake()
    {
        Instance = this;
        resumeButton.onClick.AddListener(() => 
        {
            GameManager.Instance.PauseGame();
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            LoaderScene.Load(LoaderScene.Scene.MainMenuScene);
        });
        optionButton.onClick.AddListener(() =>
        {
            Hide();
            OptionUI.Instance.Show();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

        Hide();
    }

   

    private void GameManager_OnGamePaused()
    {
        Show();
    }

    private void GameManager_OnGameUnpaused()
    {
        Hide();
    }


    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
