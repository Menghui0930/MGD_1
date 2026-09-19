using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    [SerializeField] private string levelSceneName;

    public void OnStartButtonClicked() {
        SceneController.Instance.LoadNextScene();
    }

    public void OnQuitButtonClicked() {
        Application.Quit();
    }
}