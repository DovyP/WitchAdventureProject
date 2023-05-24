using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    public List<HerbloreObjectSO> herbloreObjectSOList;
    public string recipeName;
}
