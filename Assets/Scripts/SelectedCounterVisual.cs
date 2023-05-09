using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject selectedCounterGFX;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == clearCounter)
        {
            EnableSelectedCounterGFX();
        }
        else
        {
            DisableSelectedCounterGFX();
        }
    }

    private void EnableSelectedCounterGFX()
    {
        selectedCounterGFX.SetActive(true);
    }

    private void DisableSelectedCounterGFX()
    {
        selectedCounterGFX.SetActive(false);
    }
}
