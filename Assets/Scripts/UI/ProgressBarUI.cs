using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGO;
    [SerializeField] private Image barImage;

    private IHasProgress hasProgress;

    // TODO: event to update barImage

    private void Start()
    {
        hasProgress = hasProgressGO.GetComponent<IHasProgress>();

        if (hasProgress == null)
        {
            Debug.LogError("GO: " + hasProgressGO + " doesn't have IHasProgress component");
        }

        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        // empty bar at start
        barImage.fillAmount = 0f;

        // make sure this is after setting the event, as otherwise the Start method will not be called and the event won't be set up.
        BarDeactivate();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
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
