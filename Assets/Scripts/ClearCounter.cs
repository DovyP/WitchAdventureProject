using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private HerbloreObjectSO herbloreObjectSO;
    [SerializeField] private Transform counterTop;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;

    private HerbloreObject herbloreObject;

    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T))
        {
            if (herbloreObject != null)
            {
                herbloreObject.SetClearCounter(secondClearCounter);
            }
        }
    }

    public void Interact()
    {
        if (herbloreObject == null)
        {
            Transform herbloreObjectTransform = Instantiate(herbloreObjectSO.prefab, counterTop);
            herbloreObjectTransform.GetComponent<HerbloreObject>().SetClearCounter(this);
        }
        else
        {
            Debug.Log(herbloreObject.GetClearCounter());
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