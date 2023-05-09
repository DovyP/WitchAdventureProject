using UnityEngine;

public class HerbloreObject : MonoBehaviour
{
    [SerializeField] private HerbloreObjectSO herbloreObjectSO;

    public HerbloreObjectSO GetHerbloreObjectSO() { return herbloreObjectSO; }
}