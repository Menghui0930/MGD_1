using UnityEngine;
using UnityEngine.UI;

public class IngredientUI : MonoBehaviour
{
    public int IngredientTotal;
    public int IngredientCollect = 0;

    [SerializeField] private Image IngredientImage;

    public void UpdateUI() {
        Debug.Log("COllect Ingredient");
        IngredientImage.fillAmount = (float)IngredientCollect / IngredientTotal;
    }
}
