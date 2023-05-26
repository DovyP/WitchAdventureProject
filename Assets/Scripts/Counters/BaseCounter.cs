using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IHerbloreObjectParent
{
    public static event EventHandler OnObjectDropOff;

    [SerializeField] private Transform counterTop;

    private HerbloreObject herbloreObject;

    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact();");
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

        if (herbloreObject != null)
        {
            OnObjectDropOff?.Invoke(this, EventArgs.Empty);
        }
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
