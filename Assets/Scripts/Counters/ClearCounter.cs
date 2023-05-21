using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private HerbloreObjectSO herbloreObjectSO;


    public override void Interact(Player player)
    {
        if (!HasHerbloreObject())
        {
            // no herblore object on the counter
            if (player.HasHerbloreObject())
            {
                // player has herblore object
                player.GetHerbloreObject().SetHerbloreObjectParent(this);
            }
            else
            {
                // player doesnt have herblore object
            }
        }
        else
        {
            // there is herblore object on the counter
            if (player.HasHerbloreObject())
            {
                // player has herblore object
                if(player.GetHerbloreObject().TryGetPlate(out PlateHerbloreObject plateHerbloreObject))
                {
                    // player is holding a plate
                    if (plateHerbloreObject.TryAddIngredient(GetHerbloreObject().GetHerbloreObjectSO()))
                    {
                        GetHerbloreObject().DestroySelf();
                    }
                }
                else
                {
                    // player not holding a plate but something else
                    if(GetHerbloreObject().TryGetPlate(out plateHerbloreObject))
                    {
                        // counter is holding a plate
                     if (plateHerbloreObject.TryAddIngredient(player.GetHerbloreObject().GetHerbloreObjectSO()))
                        {
                            player.GetHerbloreObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                // player doesnt have herblore object
                GetHerbloreObject().SetHerbloreObjectParent(player);
            }
        }
    }
}