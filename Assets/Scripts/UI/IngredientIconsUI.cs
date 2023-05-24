using UnityEngine;

public class IngredientIconsUI : MonoBehaviour
{
    [SerializeField] private PlateHerbloreObject plateHerbloreObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateHerbloreObject.OnIngredientAdded += PlateHerbloreObject_OnIngredientAdded;
    }

    private void PlateHerbloreObject_OnIngredientAdded(object sender, PlateHerbloreObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (HerbloreObjectSO herbloreObjectSO in plateHerbloreObject.GetHerbloreObjectSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<IngredientIconSingleUI>().SetHerbloreObjectSO(herbloreObjectSO);
        }
    }
}
