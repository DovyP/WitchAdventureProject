using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] selectedCounterGfxArray;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            EnableSelectedCounterGFX();
            animator.enabled = true;
        }
        else
        {
            DisableSelectedCounterGFX();
            animator.enabled = false;
        }
    }

    private void EnableSelectedCounterGFX()
    {
        foreach (GameObject gfxObject in selectedCounterGfxArray)
        {
            gfxObject.SetActive(true);
        }
    }

    private void DisableSelectedCounterGFX()
    {
        foreach (GameObject gfxObject in selectedCounterGfxArray)
        {
            gfxObject.SetActive(false);
        }
    }
}
