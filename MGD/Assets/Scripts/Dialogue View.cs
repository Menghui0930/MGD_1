using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DialogueView : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text dialogueText;
    public TMP_Text speakerNameText;
    public Tween activeTextTween;

    [Header("Settings")]
    public float charsPerSecond;

    public void CompleteText()
    {
        activeTextTween.Complete();
    }
    public bool IsTyping()
    {
        return (activeTextTween != null && activeTextTween.IsPlaying());
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
            .SetEase(Ease.Linear) //prints text in linear speed
            .OnComplete(() => activeTextTween = null); //resets the activeTextTween
    }
}
