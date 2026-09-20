using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;

    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private const string MasterParam = "Master";
    private const string MusicParam = "Music";
    private const string SfxParam = "SFX";

    private void Start()
    {
        mainPanel.SetActive(true);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        LoadVolumeSettings();
    }

    public void PlayGame(string sceneName)
    {
        SceneController.Instance.LoadNextScene();
    }

    public void OpenSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetMasterVolume(float sliderValue)
    {
        SetMixerVolume(MasterParam, sliderValue);
        PlayerPrefs.SetFloat(MasterParam, sliderValue);
    }

    public void SetMusicVolume(float sliderValue)
    {
        SetMixerVolume(MusicParam, sliderValue);
        PlayerPrefs.SetFloat(MusicParam, sliderValue);
    }

    public void SetSfxVolume(float sliderValue)
    {
        SetMixerVolume(SfxParam, sliderValue);
        PlayerPrefs.SetFloat(SfxParam, sliderValue);
    }

    private void SetMixerVolume(string parameterName, float sliderValue)
    {
        float volume = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat(parameterName, volume);
    }

    private void LoadVolumeSettings()
    {
            float savedMaster = PlayerPrefs.GetFloat(MasterParam, 1f);
            masterSlider.value = savedMaster;
            SetMixerVolume(MasterParam, savedMaster);

            float savedMusic = PlayerPrefs.GetFloat(MusicParam, 1f);
            musicSlider.value = savedMusic;
            SetMixerVolume(MusicParam, savedMusic);
            
            float savedSfx = PlayerPrefs.GetFloat(SfxParam, 1f);
            sfxSlider.value = savedSfx;
            SetMixerVolume(SfxParam, savedSfx);
    }
}
