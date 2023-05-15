using UnityEngine;

public class BoilingCounterVisual : MonoBehaviour
{
    [SerializeField] private BoilingCounter boilingCounter;
    [SerializeField] private GameObject fireFXParticles;

    private void Start()
    {
        boilingCounter.OnStateChanged += BoilingCounter_OnStateChanged;
    }

    private void BoilingCounter_OnStateChanged(object sender, BoilingCounter.OnStateChangedEventArgs e)
    {
        bool isEnabled = e.currentState == BoilingCounter.State.Boiling || e.currentState == BoilingCounter.State.Boiled;
        fireFXParticles.SetActive(isEnabled);
    }
}
