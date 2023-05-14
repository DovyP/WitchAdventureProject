using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerTakeFromContainer;

    [SerializeField] private HerbloreObjectSO herbloreObjectSO;


    public override void Interact(Player player)
    {
        if (!player.HasHerbloreObject())
        {
            // player doesnt have herblore object so it can be given to the player
            HerbloreObject.SpawnHerbloreObject(herbloreObjectSO, player);

            OnPlayerTakeFromContainer?.Invoke(this, EventArgs.Empty);
        }
    }
}
