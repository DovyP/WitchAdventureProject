using UnityEngine;

public class HerbloreObject : MonoBehaviour
{
    [SerializeField] private HerbloreObjectSO herbloreObjectSO;

    private IHerbloreObjectParent herbloreObjectParent;

    public HerbloreObjectSO GetHerbloreObjectSO() { return herbloreObjectSO; }

    public void SetHerbloreObjectParent(IHerbloreObjectParent herbloreObjectParent)
    {
        if(this.herbloreObjectParent != null)
        {
            this.herbloreObjectParent.ClearHerbloreObject();
        }

        this.herbloreObjectParent = herbloreObjectParent;

        if (herbloreObjectParent.HasHerbloreObject())
        {
            Debug.LogError("IHerbloreObjectParent already has a HerbloreObject!");
        }

        herbloreObjectParent.SetHerbloreObject(this);

        transform.parent = herbloreObjectParent.GetHerbloreObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public IHerbloreObjectParent GetHerbloreObjectParent() { return herbloreObjectParent; }

    public void DestroySelf()
    {
        herbloreObjectParent.ClearHerbloreObject();
        Destroy(gameObject);
    }

    public static HerbloreObject SpawnHerbloreObject(HerbloreObjectSO herbloreObjectSO, IHerbloreObjectParent herbloreObjectParent)
    {
        Transform herbloreObjectTransform = Instantiate(herbloreObjectSO.prefab);
        HerbloreObject herbloreObject = herbloreObjectTransform.GetComponent<HerbloreObject>();

        herbloreObject.SetHerbloreObjectParent(herbloreObjectParent);

        return herbloreObject;
    }
}