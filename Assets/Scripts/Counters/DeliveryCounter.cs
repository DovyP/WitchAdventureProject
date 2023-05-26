using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    // only 1 instance of delivery counter in the game, singleton.
    public static DeliveryCounter instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public override void Interact(Player player)
    {
        if (player.HasHerbloreObject())
        {
            if(player.GetHerbloreObject().TryGetPlate(out PlateHerbloreObject plateHerbloreObject))
            {
                // delivery counter takes plates

                DeliveryManager.instance.DeliverRecipe(plateHerbloreObject);
                player.GetHerbloreObject().DestroySelf();
            }
        }
    }
}
