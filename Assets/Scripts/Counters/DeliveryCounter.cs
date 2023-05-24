using UnityEngine;

public class DeliveryCounter : BaseCounter
{
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
