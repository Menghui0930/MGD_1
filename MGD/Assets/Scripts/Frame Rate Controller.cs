using UnityEngine;

public class FrameRateController : MonoBehaviour
{
[SerializeField] private int targetFrameRate = 60;

    void Awake()
    {
        // Make this GameObject persist across scene loads
        DontDestroyOnLoad(gameObject);

        // Apply frame rate configuration
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }
}
