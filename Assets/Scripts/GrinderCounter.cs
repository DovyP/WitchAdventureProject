using UnityEngine;

public class GrinderCounter : BaseCounter
{
    [SerializeField] private HerbloreObjectSO grindedHerbloreObjectSO;

    public override void Interact(Player player)
    {
        // add grinder logic here
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
            }
            else
            {
                // player doesnt have herblore object
                GetHerbloreObject().SetHerbloreObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasHerbloreObject())
        {
            // there is a herblore object on the counter
            GetHerbloreObject().DestroySelf();

            HerbloreObject.SpawnHerbloreObject(grindedHerbloreObjectSO, this);
        }
    }
}
