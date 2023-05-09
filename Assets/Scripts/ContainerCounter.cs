using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerTakeFromContainer;

    [SerializeField] private HerbloreObjectSO herbloreObjectSO;


    public override void Interact(Player player)
    {
        Transform herbloreObjectTransform = Instantiate(herbloreObjectSO.prefab);
        herbloreObjectTransform.GetComponent<HerbloreObject>().SetHerbloreObjectParent(player);
        OnPlayerTakeFromContainer?.Invoke(this, EventArgs.Empty);
    }
}
