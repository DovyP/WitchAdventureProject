using UnityEngine;

public class BaseCounter : MonoBehaviour, IHerbloreObjectParent
{
    [SerializeField] private Transform counterTop;

    private HerbloreObject herbloreObject;

    public virtual void Interact(Player player)
    {

    }

    public virtual void InteractAlternate(Player player)
    {

    }

    public Transform GetHerbloreObjectFollowTransform()
    {
        return counterTop;
    }

    public void SetHerbloreObject(HerbloreObject herbloreObject)
    {
        this.herbloreObject = herbloreObject;
    }

    public HerbloreObject GetHerbloreObject()
    {
        return herbloreObject;
    }

    public void ClearHerbloreObject()
    {
        herbloreObject = null;
    }

    public bool HasHerbloreObject()
    {
        return herbloreObject != null;
    }
}
