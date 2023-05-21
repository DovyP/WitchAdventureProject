using System;
using UnityEngine;

public class BoilingCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State currentState;
    }

    public enum State
    {
        Idle,
        Boiling,
        Boiled,
        Ruined,
    }

    [SerializeField] private BoilingRecipeSO[] boilingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private BoilingRecipeSO boilingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private float boilingTimer;
    private float burningTimer;

    private State currentState;

    private void Start()
    {
        currentState = State.Idle;
    }

    private void Update()
    {
        if (HasHerbloreObject())
        {
            switch (currentState)
            {
                case State.Idle:
                    break;
                case State.Boiling:
                    boilingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = boilingTimer / boilingRecipeSO.boilingTimerMax
                    });

                    if (boilingTimer >= boilingRecipeSO.boilingTimerMax)
                    {
                        // boiled
                        GetHerbloreObject().DestroySelf();

                        HerbloreObject.SpawnHerbloreObject(boilingRecipeSO.output, this);

                        currentState = State.Boiled;

                        burningTimer = 0f;

                        burningRecipeSO = GetBurningRecipeSOWithInput(GetHerbloreObject().GetHerbloreObjectSO());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            currentState = currentState
                        });
                    }
                    break;
                case State.Boiled:
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });

                    if (burningTimer >= burningRecipeSO.burningTimerMax)
                    {
                        // ruined
                        GetHerbloreObject().DestroySelf();

                        HerbloreObject.SpawnHerbloreObject(burningRecipeSO.output, this);

                        currentState = State.Ruined;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            currentState = currentState
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                    break;
                case State.Ruined:
                    break;
            }
        }
    }

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
                    // player has herblore object that can be boiled
                    player.GetHerbloreObject().SetHerbloreObjectParent(this);

                    boilingRecipeSO = GetBoilingRecipeSOWithInput(GetHerbloreObject().GetHerbloreObjectSO());

                    currentState = State.Boiling;

                    boilingTimer = 0f;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        currentState = currentState
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = boilingTimer / boilingRecipeSO.boilingTimerMax
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
                if (player.GetHerbloreObject().TryGetPlate(out PlateHerbloreObject plateHerbloreObject))
                {
                    // player is holding a plate
                    if (plateHerbloreObject.TryAddIngredient(GetHerbloreObject().GetHerbloreObjectSO()))
                    {
                        GetHerbloreObject().DestroySelf();


                        currentState = State.Idle;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            currentState = currentState
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                }
            }
            else
            {
                // player doesnt have herblore object
                GetHerbloreObject().SetHerbloreObjectParent(player);

                currentState = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    currentState = currentState
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
        }
    }

    private bool HasRecipeWithInput(HerbloreObjectSO inputHerbloreObjectSO)
    {
        BoilingRecipeSO boilingRecipeSO = GetBoilingRecipeSOWithInput(inputHerbloreObjectSO);
        return boilingRecipeSO != null;
    }

    private HerbloreObjectSO GetOutputFromInput(HerbloreObjectSO inputHerbloreObjectSO)
    {
        BoilingRecipeSO boilingRecipeSO = GetBoilingRecipeSOWithInput(inputHerbloreObjectSO);

        if (boilingRecipeSO != null)
        {
            return boilingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private BoilingRecipeSO GetBoilingRecipeSOWithInput(HerbloreObjectSO inputHerbloreObjectSO)
    {
        foreach (BoilingRecipeSO boilingRecipeSO in boilingRecipeSOArray)
        {
            if (boilingRecipeSO.input == inputHerbloreObjectSO)
            {
                return boilingRecipeSO;
            }
        }

        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(HerbloreObjectSO inputHerbloreObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputHerbloreObjectSO)
            {
                return burningRecipeSO;
            }
        }

        return null;
    }
}
