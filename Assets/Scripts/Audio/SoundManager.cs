using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipReferencesSO audioClipReferencesSO;

    private void Start()
    {
        DeliveryManager.instance.OnRecipeDeliveryCompleted += DeliveryManager_OnRecipeDeliveryCompleted;
        DeliveryManager.instance.OnRecipeDeliveryFailed += DeliveryManager_OnRecipeDeliveryFailed;
        GrinderCounter.OnAnyGrind += GrinderCounter_OnAnyGrind;
        Player.Instance.OnPickUpObject += Player_OnPickUpObject;
        BaseCounter.OnObjectDropOff += BaseCounter_OnObjectDropOff;
        TrashCounter.OnObjectDestroy += TrashCounter_OnObjectDestroy;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void TrashCounter_OnObjectDestroy(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = (TrashCounter)sender;
        PlaySound(audioClipReferencesSO.objectDrop, trashCounter.transform.position);
    }

    private void BaseCounter_OnObjectDropOff(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = (BaseCounter)sender;
        PlaySound(audioClipReferencesSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickUpObject(object sender, System.EventArgs e)
    {
        PlaySound(audioClipReferencesSO.objectPickup, Player.Instance.transform.position);
    }

    private void GrinderCounter_OnAnyGrind(object sender, System.EventArgs e)
    {
        GrinderCounter grinderCounter = (GrinderCounter)sender;
        PlaySound(audioClipReferencesSO.grind, grinderCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeDeliveryFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.instance;

        PlaySound(audioClipReferencesSO.deliveryFailed, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeDeliveryCompleted(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.instance;

        PlaySound(audioClipReferencesSO.deliveryCompleted, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayFootstepsSound(Vector3 position, float volume)
    {
        PlaySound(audioClipReferencesSO.footstep, position, volume);
    }
}
