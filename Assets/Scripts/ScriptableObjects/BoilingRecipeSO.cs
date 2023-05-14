using UnityEngine;

[CreateAssetMenu()]
public class BoilingRecipeSO : ScriptableObject
{
    public HerbloreObjectSO input;
    public HerbloreObjectSO output;
    public float boilingTimerMax;
}
