using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GrinderCounter grinderCounter;
    [SerializeField] private Image barImage;

    // TODO: event to update barImage

    private void Start()
    {
        grinderCounter.OnProgressChanged += GrinderCounter_OnProgressChanged;

        // empty bar at start
        barImage.fillAmount = 0f;

        // make sure this is after setting the event, as otherwise the Start method will not be called and the event won't be set up.
        BarDeactivate();
    }

    private void GrinderCounter_OnProgressChanged(object sender, GrinderCounter.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if(e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            BarDeactivate();
        }
        else
        {
            BarActivate();
        }
    }

    private void BarActivate()
    {
        gameObject.SetActive(true);
    }

    private void BarDeactivate()
    {
        gameObject.SetActive(false);
    }
}
