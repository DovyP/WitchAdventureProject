using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private HerbloreObjectSO herbloreObjectSO;
    [SerializeField] private Transform counterTop;

    public void Interact()
    {
        Transform herbloreObjectTransform = Instantiate(herbloreObjectSO.prefab, counterTop);
        herbloreObjectTransform.localPosition = Vector3.zero;
        Debug.Log(herbloreObjectTransform.GetComponent<HerbloreObject>().GetHerbloreObjectSO().objectName);
    }
}