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
            }
            else
            {
                // player doesnt have herblore object
                GetHerbloreObject().SetHerbloreObjectParent(player);
            }
        }
    }
}