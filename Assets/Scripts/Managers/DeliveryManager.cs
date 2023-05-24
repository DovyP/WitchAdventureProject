using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    public static DeliveryManager instance {  get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;


    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;

    private void Awake()
    {
        instance = this;

        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipesMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
                waitingRecipeSOList.Add(waitingRecipeSO);
            }
        }
    }

    public void DeliverRecipe(PlateHerbloreObject plateHerbloreObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if(waitingRecipeSO.herbloreObjectSOList.Count == plateHerbloreObject.GetHerbloreObjectSOList().Count)
            {
                // has the same number of ingredients
                bool plateIngredientsMatchTheRecipe = true;
                foreach (HerbloreObjectSO recipeHerbloreObjectSO in waitingRecipeSO.herbloreObjectSOList)
                {
                    // cycling through all ingredients in the recipe
                    bool ingredientFound = false;
                    foreach (HerbloreObjectSO plateHerbloreObjectSO in plateHerbloreObject.GetHerbloreObjectSOList())
                    {
                        // cycling through all ingredients in the plate
                        if(plateHerbloreObjectSO == recipeHerbloreObjectSO)
                        {
                            // validate if all ingredients are correct
                            // in this case the ingredients match
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        // this recipe ingredient is not on the plate
                        plateIngredientsMatchTheRecipe = false;
                    }
                }

                if (plateIngredientsMatchTheRecipe)
                {
                    // player delivered the correct recipe
                    Debug.Log("player delivered the correct recipe");
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }

        // no match found
        // incorrect recipe
        Debug.Log("Incorrect recipe");
    }
}