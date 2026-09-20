using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{

    public Button button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        button.onClick.AddListener(ReturnToMainMenu);
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    void ReturnToMainMenu()
    {
        SceneController.Instance.LoadMainMenu();
    }
}
