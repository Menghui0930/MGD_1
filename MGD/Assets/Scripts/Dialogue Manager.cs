using System;
using System.Collections.Generic;
using DG.Tweening;
using Ink.Runtime;
using Ink.UnityIntegration;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [Header("Ink Settings")]
    public InkFile inkFile;
    private Story story;
    public RectTransform characterPortraitPlayer;
    public RectTransform characterPortraitNPC;

    public InputAction debugKey;

    [Header("Components")]
    public DialogueView dialogueView;
    public CharacterView characterView;
    
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
        if (dialogueView.IsTyping())
        {
            dialogueView.CompleteText();
            return;
        }

        if (!story.canContinue) return;

        string text = story.Continue();
        ProcessTags(story.currentTags);
        dialogueView.AnimateText(text);

    }

    public void ProcessTags(List<String> tags)
    {
        string currentSpeaker = "";
        string currentSlot = "Left";
        string currentEmotion = "default";

        foreach (string tag in tags)
        {
            string[] splitTag = tag.Split(":");
            string key = splitTag[0].Trim();
            string value = splitTag.Length > 1? splitTag[1].Trim() : "";

            switch (key)
            {
                case "speaker":
                dialogueView.SetSpeaker(value);
                currentSpeaker = value;
                break;

                case "slot":
                currentSlot = value;
                break;

                case "emotion":
                currentEmotion = value;
                break;
            }
        }

        if (!string.IsNullOrEmpty(currentSpeaker))
        {
            characterView.SetCharacter(currentSpeaker,currentSlot,currentEmotion);
            characterView.HighlightSpeaker(currentSpeaker);
        }

    }
}
