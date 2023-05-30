using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button menuButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ToggleGamePause();
        });

        menuButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.MenuScene);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePause += GameManager_OnGamePause;
        GameManager.Instance.OnGameResume += GameManager_OnGameResume;

        GamePauseUIDisable();
    }

    private void GameManager_OnGameResume(object sender, System.EventArgs e)
    {
        GamePauseUIDisable();
    }

    private void GameManager_OnGamePause(object sender, System.EventArgs e)
    {
        GamePausedUIEnable();
    }

    private void GamePausedUIEnable()
    {
        gameObject.SetActive(true);
    }

    private void GamePauseUIDisable()
    {
        gameObject.SetActive(false);
    }
}
