using UnityEngine;
using UnityEngine.UI;

public class IngredientUI : MonoBehaviour
{
    public int IngredientTotal;
    public int IngredientCollect = 0;
    public PopUpUI popup;

    [SerializeField] private Image IngredientImage;

    public void UpdateUI() {
        Debug.Log("Collect Ingredient");
        IngredientImage.fillAmount = (float)IngredientCollect / IngredientTotal;
        if (IngredientCollect == IngredientTotal)
        Invoke(nameof(OnFullyCollected), 0.2f); 
    }

    public void OnFullyCollected()
    {
        popup.StartAnimation();
    }
}
