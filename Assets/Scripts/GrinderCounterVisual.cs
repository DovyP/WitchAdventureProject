using UnityEngine;

public class GrinderCounterVisual : MonoBehaviour
{
    private const string GRIND = "Grind";

    [SerializeField] private GrinderCounter grinderCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        grinderCounter.OnGrind += GrinderCounter_OnGrind;
    }

    private void GrinderCounter_OnGrind(object sender, System.EventArgs e)
    {
        animator.SetTrigger(GRIND);
    }
}
