using UnityEngine;

public class PickUPIngredient : MonoBehaviour
{
    public IngredientUI ingredientUI;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            ingredientUI.IngredientCollect++;
            ingredientUI.UpdateUI();

            transform.gameObject.SetActive(false);
        }   
    }
}
