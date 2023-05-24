using UnityEngine;
using UnityEngine.UI;

public class IngredientIconSingleUI : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetHerbloreObjectSO(HerbloreObjectSO herbloreObjectSO)
    {
        image.sprite = herbloreObjectSO.iconSprite;
    }
}
