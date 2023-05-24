using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateHerbloreObject : HerbloreObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs: EventArgs
    {
        public HerbloreObjectSO herbloreObjectSO;
    }

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

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                herbloreObjectSO = herbloreObjectSO
            });

            return true;
        }
    }

    public List<HerbloreObjectSO> GetHerbloreObjectSOList()
    {
        return herbloreObjectSOList;
    }
}
