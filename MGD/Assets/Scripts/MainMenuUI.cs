using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    [SerializeField] private string levelSceneName;

    public void OnStartButtonClicked() {
        SceneManager.LoadScene(levelSceneName);
    }

    public void OnQuitButtonClicked() {
        Application.Quit();
    }
}