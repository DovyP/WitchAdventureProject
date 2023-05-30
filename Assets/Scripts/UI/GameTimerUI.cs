using UnityEngine;
using UnityEngine.UI;

public class GameTimerUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;

    private void Update()
    {
        timerImage.fillAmount = GameManager.Instance.GetGameTimerNormalized();
    }
}
