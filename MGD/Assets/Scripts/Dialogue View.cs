using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DialogueView : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text dialogueText;
    public TMP_Text speakerNameText;
   

    [Header("Settings")]
    public float charsPerSecond;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip blipSound;
    public int charInterval = 2;
    [Range(0f, 1f)] public float pitchVariance = 0.1f;


    public Tween activeTextTween;
    private int lastVisibleCharCount;

    public void CompleteText()
    {
        if (activeTextTween != null && activeTextTween.IsActive())
        {
            activeTextTween.Complete();
                activeTextTween = null; 
        }
    }
    public bool IsTyping()
    {
        return activeTextTween != null && activeTextTween.IsPlaying();
    }

    public void SetSpeaker(string speakerName)
    {
        if (speakerNameText != null)
            speakerNameText.text = speakerName;
    }
    public void AnimateText(string fullText)
    {
        activeTextTween?.Kill();
        dialogueText.text = fullText;
        dialogueText.maxVisibleCharacters = 0;

        int totalChars = fullText.Length;
        activeTextTween = DOTween.To( 
            x => dialogueText.maxVisibleCharacters = (int)x, //setter, return value is x
            0, //mininum characters
            totalChars, //maximum
            totalChars/charsPerSecond) //duration
            .SetEase(Ease.Linear)
            .OnUpdate(PlayBlipSound) //prints text in linear speed
            .OnComplete(() => {activeTextTween = null; lastVisibleCharCount = 0;}); //resets the activeTextTween
    }

    private void PlayBlipSound()
    {
        int currentCount = dialogueText.maxVisibleCharacters;

        // Only play on new character
        if (currentCount > lastVisibleCharCount)
        {
            lastVisibleCharCount = currentCount;

            if (currentCount % charInterval == 0)
            {
            audioSource.pitch = Random.Range(1f - pitchVariance, 1f + pitchVariance);
            audioSource.PlayOneShot(blipSound);
            }

        }
    }
}
