using System;
using UnityEngine;

public class GrinderCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;

    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    public event EventHandler OnGrind;

    [SerializeField] private GrindingRecipeSO[] grindingRecipeSOArray;

    private int grindingProgress;

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
                    grindingProgress = 0;

                    GrindingRecipeSO grindingRecipeSO = GetGrindingRecipeSOWithInput(GetHerbloreObject().GetHerbloreObjectSO());

                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        // make sure to avoid converting into int, casting one variable as (float)
                        progressNormalized = (float)grindingProgress / grindingRecipeSO.grindingProgressMax
                    });
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
            grindingProgress++;

            OnGrind?.Invoke(this, EventArgs.Empty);

            GrindingRecipeSO grindingRecipeSO = GetGrindingRecipeSOWithInput(GetHerbloreObject().GetHerbloreObjectSO());

            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                // make sure to avoid converting into int, casting one variable as (float)
                progressNormalized = (float)grindingProgress / grindingRecipeSO.grindingProgressMax
            });

            if (grindingProgress >= grindingRecipeSO.grindingProgressMax)
            {
                HerbloreObjectSO outputHerbloreObjectSO = GetOutputFromInput(GetHerbloreObject().GetHerbloreObjectSO());

                GetHerbloreObject().DestroySelf();

                HerbloreObject.SpawnHerbloreObject(outputHerbloreObjectSO, this);
            }
        }
    }

    private bool HasRecipeWithInput(HerbloreObjectSO inputHerbloreObjectSO)
    {
        GrindingRecipeSO grindingRecipeSO = GetGrindingRecipeSOWithInput(inputHerbloreObjectSO);
        return grindingRecipeSO != null;
    }

    private HerbloreObjectSO GetOutputFromInput(HerbloreObjectSO inputHerbloreObjectSO)
    {
        GrindingRecipeSO grindingRecipeSO = GetGrindingRecipeSOWithInput(inputHerbloreObjectSO);

        if(grindingRecipeSO != null)
        {
            return grindingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private GrindingRecipeSO GetGrindingRecipeSOWithInput(HerbloreObjectSO inputHerbloreObjectSO)
    {
        foreach (GrindingRecipeSO grindingRecipeSO in grindingRecipeSOArray)
        {
            if (grindingRecipeSO.input == inputHerbloreObjectSO)
            {
                return grindingRecipeSO;
            }
        }

        return null;
    }
}
