using System;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnObjectDestroy;

    new public static void ResetStaticData()
    {
        OnObjectDestroy = null;
    }

    public override void Interact(Player player)
    {
        if (player.HasHerbloreObject())
        {
            player.GetHerbloreObject().DestroySelf();

            OnObjectDestroy?.Invoke(this, EventArgs.Empty);
        }
    }
}
