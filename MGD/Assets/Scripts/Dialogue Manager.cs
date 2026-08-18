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
        foreach (string tag in tags)
        {
            string[] splitTag = tag.Split(":");
            string key = splitTag[0].Trim();
            string value = splitTag.Length > 1? splitTag[1].Trim() : "";

            switch (key)
            {
                case "speaker":
                dialogueView.SetSpeaker(value);
                break;

                case "animation":
                break;
            }
        }

    }


    // private void SetActiveCharacter(string characterName)
    // {
    //     if (activeCharacter.characterName == characterName) return;

    //     foreach (Character character in characters)
    //     {
    //         if (character.characterName == characterName)
    //         {
    //             activeCharacter = character;
    //             RectTransform spriteToUnfade = activeCharacter.characterSprite[activeCharacter.currentSpriteIndex].location;
                
    //         }
                
    //     }
    //     speakerNameText.text = characterName;


    // }
}
