using System;
using System.Collections.Generic;
using DG.Tweening;
using Ink.Runtime;
using Ink.UnityIntegration;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public InputAction debugKey;

    [Header("Ink Settings")]
    public InkFile inkFile;
    private Story story;


    [Header("UI Elements")]
    public TMP_Text dialogueText;
    public TMP_Text speakerNameText;
    public RectTransform characterPortraitPlayer;
    public RectTransform characterPortraitNPC;


    private Tween activeTextTween;
    public float charactersPerSecond;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        debugKey.Enable();
        story = new Story(inkFile.storyJson);
    }

    void Update()
    {
        if(debugKey.triggered)
        {
            Debug.Log("hi");
            DisplayNextLine();
        }
            
    }

    // Update is called once per frame
    public void DisplayNextLine()
    {
        if (activeTextTween != null && activeTextTween.IsPlaying())
        {
            activeTextTween.Complete();
            return;
        }

        if (!story.canContinue) return;

        string text = story.Continue();
        ProcessTags(story.currentTags);
        AnimateText(text);

    }

    public void ProcessTags(List<String> tags)
    {
        foreach (string tag in tags)
        {
            string[] splitTag = tag.Split(":");
            string key = splitTag[0].Trim();
            string value = splitTag.Length > 1? splitTag[1].Trim() : "";

            switch (key)
            {
                case "speaker":
                speakerNameText.text = value;
                break;

                case "animation":
                break;
            }
        }

    }

    private void AnimateText(string fullText)
    {
        activeTextTween?.Kill();
            
        dialogueText.text = fullText;
        dialogueText.maxVisibleCharacters = 0;

        

        int totalChars = fullText.Length;
        activeTextTween = DOTween.To( 
            x => dialogueText.maxVisibleCharacters = (int)x, //setter, return value is x
            0, //mininum characters
            totalChars, //maximum
            totalChars/charactersPerSecond) //duration
            .SetEase(Ease.Linear) //prints text in linear speed
            .OnComplete(() => activeTextTween = null); //resets the activeTextTween
    }
}
