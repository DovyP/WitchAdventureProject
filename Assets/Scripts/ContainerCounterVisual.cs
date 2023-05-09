using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private const string TAKE_TRIGGER = "TakeTrigger";

    [SerializeField] private ContainerCounter containerCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //ss
    }

    private void Start()
    {
        containerCounter.OnPlayerTakeFromContainer += ContainerCounter_OnPlayerTakeFromContainer;
    }

    private void ContainerCounter_OnPlayerTakeFromContainer(object sender, System.EventArgs e)
    {
        animator.SetTrigger(TAKE_TRIGGER);
    }
}
