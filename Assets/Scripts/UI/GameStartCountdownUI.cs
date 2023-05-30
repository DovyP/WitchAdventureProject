using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI startCountdownText;

    private void Start()
    {
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;

        StartCountdownDisable();
    }

    private void GameManager_OnStateChange(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameStarting())
        {
            StartCountdownEnable();
        }
        else
        {
            StartCountdownDisable();
        }
    }

    private void Update()
    {
        startCountdownText.text = Mathf.Ceil(GameManager.Instance.GetStartCountdown()).ToString();
    }

    private void StartCountdownEnable()
    {
        gameObject.SetActive(true);
    }
    
    private void StartCountdownDisable()
    {
        gameObject.SetActive(false);
    }
}
