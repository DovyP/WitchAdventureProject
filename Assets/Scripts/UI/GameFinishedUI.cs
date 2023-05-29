using TMPro;
using UnityEngine;

public class GameFinishedUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        GameManager.instance.OnStateChange += GameManager_OnStateChange;

        GameFinishedUIDisable();
    }

    private void GameManager_OnStateChange(object sender, System.EventArgs e)
    {
        if (GameManager.instance.IsGameFinished())
        {
            GameFinishedUIEnable();

            scoreText.text = DeliveryManager.instance.GetRecipesDelivered().ToString();
        }
        else
        {
            GameFinishedUIDisable();
        }
    }

    private void GameFinishedUIEnable()
    {
        gameObject.SetActive(true);
    }

    private void GameFinishedUIDisable()
    {
        gameObject.SetActive(false);
    }
}
