using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private HerbloreObjectSO plateSO;

    private float plateSpawnTimer;
    private float plateSpawnTimerMax = 4f;

    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax = 4;

    private void Update()
    {
        plateSpawnTimer += Time.deltaTime;

        if(plateSpawnTimer > plateSpawnTimerMax)
        {
            plateSpawnTimer = 0;

            if(platesSpawnedAmount < platesSpawnedAmountMax)
            {
                platesSpawnedAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasHerbloreObject())
        {
            // player does not have herblore object
            if(platesSpawnedAmount > 0)
            {
                // there's at least 1 plate on the counter 
                platesSpawnedAmount--;

                HerbloreObject.SpawnHerbloreObject(plateSO, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
