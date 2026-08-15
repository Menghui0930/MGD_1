using System;
using System.Collections.Generic;
using DG.Tweening;
using Ink.Runtime;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    [Header("Ink Settings")]
    public TextAsset inkJsonAsset;
    private Story story;


    [Header("UI Elements")]
    public TMP_Text dialogueText;
    public TMP_Text speakerNameText;
    public RectTransform characterPortraitPlayer;
     public RectTransform characterPortraitNPC;

     private Tween activeTextTween;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        story = new Story(inkJsonAsset.text);
    }

    // Update is called once per frame
    public void DisplayNextLine()
    {
        if (!story.canContinue) return;
        string text = story.Continue().Trim();
    }

    public void ProcessTags(List<String> tags)
    {
        foreach (string tag in tags)
        {
            
        }

    }
}
