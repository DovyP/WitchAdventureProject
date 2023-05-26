using UnityEngine;

public class BoilingCounterSound : MonoBehaviour
{
    [SerializeField] private BoilingCounter boilingCounter;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        boilingCounter.OnStateChanged += BoilingCounter_OnStateChanged;
    }

    private void BoilingCounter_OnStateChanged(object sender, BoilingCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.currentState == BoilingCounter.State.Boiling || e.currentState == BoilingCounter.State.Boiled;
        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
