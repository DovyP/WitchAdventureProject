using UnityEngine;

public interface IHerbloreObjectParent
{
    public Transform GetHerbloreObjectFollowTransform();

    public void SetHerbloreObject(HerbloreObject herbloreObject);

    public HerbloreObject GetHerbloreObject();

    public void ClearHerbloreObject();

    public bool HasHerbloreObject();
}
