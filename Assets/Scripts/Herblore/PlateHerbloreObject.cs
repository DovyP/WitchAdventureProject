using System.Collections.Generic;
using UnityEngine;

public class PlateHerbloreObject : HerbloreObject
{
    [SerializeField] private List<HerbloreObjectSO> validHerbloreObjectSOList;

    private List<HerbloreObjectSO> herbloreObjectSOList;

    private void Awake()
    {
        herbloreObjectSOList = new List<HerbloreObjectSO>();
    }

    public bool TryAddIngredient(HerbloreObjectSO herbloreObjectSO)
    {
        if (!validHerbloreObjectSOList.Contains(herbloreObjectSO))
        {
            // not a valid ingredient
            return false;
        }

        if (herbloreObjectSOList.Contains(herbloreObjectSO))
        {
            // duplicate
            return false;
        } else
        {
            herbloreObjectSOList.Add(herbloreObjectSO);
            return true;
        }
    }
}
