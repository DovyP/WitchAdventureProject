using UnityEngine;

public class GrinderCounter : BaseCounter
{
    [SerializeField] private GrindingRecipeSO[] grindingRecipeSOArray;

    public override void Interact(Player player)
    {
        // add grinder logic here
        if (!HasHerbloreObject())
        {
            // no herblore object on the counter
            if (player.HasHerbloreObject())
            {
                // player has herblore object
                if (HasRecipeWithInput(player.GetHerbloreObject().GetHerbloreObjectSO()))
                {
                    // player has herblore object that can be grinded
                    player.GetHerbloreObject().SetHerbloreObjectParent(this);
                }
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
        if (HasHerbloreObject() && HasRecipeWithInput(GetHerbloreObject().GetHerbloreObjectSO()))
        {
            // there is a herblore object on the counter and it can be grinded
            HerbloreObjectSO outputHerbloreObjectSO = GetOutputFromInput(GetHerbloreObject().GetHerbloreObjectSO());
            
            GetHerbloreObject().DestroySelf();

            HerbloreObject.SpawnHerbloreObject(outputHerbloreObjectSO, this);
        }
    }

    private bool HasRecipeWithInput(HerbloreObjectSO inputHerbloreObjectSO)
    {
        foreach (GrindingRecipeSO grindingRecipeSO in grindingRecipeSOArray)
        {
            if (grindingRecipeSO.input == inputHerbloreObjectSO)
            {
                // has recipe
                return true;
            }
        }
        // doesnt have recipe
        return false;
    }

    private HerbloreObjectSO GetOutputFromInput(HerbloreObjectSO inputHerbloreObjectSO)
    {
        foreach(GrindingRecipeSO grindingRecipeSO in grindingRecipeSOArray)
        {
            if(grindingRecipeSO.input == inputHerbloreObjectSO)
            {
                return grindingRecipeSO.output;
            }
        }

        return null;
    }
}
