using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine;

public class PopUpUI : MonoBehaviour
{
    public RectTransform rectTransform;
    public AudioSource audioSource;
    public AudioClip allItemsCollected;
    public Image image;
    public Button button;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonPress);
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    public void StartAnimation()
    {
        image.enabled = true;
        audioSource.PlayOneShot(allItemsCollected);
        rectTransform.DOAnchorPos(Vector2.zero, 0.5f);
    }

    public void OnButtonPress()
    {
        SceneController.Instance.LoadNextScene();
    }

    
}
