using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipeSO : ScriptableObject
{
    public HerbloreObjectSO input;
    public HerbloreObjectSO output;
    public float burningTimerMax;
}
