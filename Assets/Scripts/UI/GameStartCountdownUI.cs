using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI startCountdownText;

    private void Start()
    {
        GameManager.instance.OnStateChange += GameManager_OnStateChange;

        StartCountdownDisable();
    }

    private void GameManager_OnStateChange(object sender, System.EventArgs e)
    {
        if (GameManager.instance.IsGameStarting())
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
        startCountdownText.text = Mathf.Ceil(GameManager.instance.GetStartCountdown()).ToString();
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
