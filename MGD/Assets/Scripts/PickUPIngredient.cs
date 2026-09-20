using UnityEngine;

public class PickUPIngredient : MonoBehaviour
{
    public IngredientUI ingredientUI;
    public AudioSource audioSource;
    public AudioClip pickUp;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            ingredientUI.IngredientCollect++;
            ingredientUI.UpdateUI();
            audioSource.PlayOneShot(pickUp);

            transform.gameObject.SetActive(false);
        }   
    }
}
