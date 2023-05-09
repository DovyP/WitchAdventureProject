using UnityEngine;

public class ClearCounter : MonoBehaviour, IHerbloreObjectParent
{
    [SerializeField] private HerbloreObjectSO herbloreObjectSO;
    [SerializeField] private Transform counterTop;

    private HerbloreObject herbloreObject;


    public void Interact(Player player)
    {
        if (herbloreObject == null)
        {
            Transform herbloreObjectTransform = Instantiate(herbloreObjectSO.prefab, counterTop);
            herbloreObjectTransform.GetComponent<HerbloreObject>().SetHerbloreObjectParent(this);
        }
        else
        {
            // take object/give to player
            herbloreObject.SetHerbloreObjectParent(player);
        }
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