using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct HerbloreObjectSO_GameObject
    {
        public HerbloreObjectSO herbloreObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateHerbloreObject plateHerbloreObject;
    [SerializeField] private List<HerbloreObjectSO_GameObject> herbloreObjectSO_GameObjectList;

    private void Start()
    {
        plateHerbloreObject.OnIngredientAdded += PlateHerbloreObject_OnIngredientAdded;

        foreach (HerbloreObjectSO_GameObject herbloreObjectSO_GameObject in herbloreObjectSO_GameObjectList)
        {
            herbloreObjectSO_GameObject.gameObject.SetActive(false);
        }
    }

    private void PlateHerbloreObject_OnIngredientAdded(object sender, PlateHerbloreObject.OnIngredientAddedEventArgs e)
    {
        foreach(HerbloreObjectSO_GameObject herbloreObjectSO_GameObject in herbloreObjectSO_GameObjectList)
        {
            if (herbloreObjectSO_GameObject.herbloreObjectSO == e.herbloreObjectSO)
            {
                herbloreObjectSO_GameObject.gameObject.SetActive(true);
            }
        }
    }
}
