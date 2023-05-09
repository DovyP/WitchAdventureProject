using UnityEngine;

public class HerbloreObject : MonoBehaviour
{
    [SerializeField] private HerbloreObjectSO herbloreObjectSO;

    private ClearCounter clearCounter;

    public HerbloreObjectSO GetHerbloreObjectSO() { return herbloreObjectSO; }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        if(this.clearCounter != null)
        {
            this.clearCounter.ClearHerbloreObject();
        }

        this.clearCounter = clearCounter;

        if (clearCounter.HasHerbloreObject())
        {
            Debug.LogError("Counter already has a HerbloreObject!");
        }

        clearCounter.SetHerbloreObject(this);

        transform.parent = clearCounter.GetHerbloreObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public ClearCounter GetClearCounter() { return clearCounter; }
}