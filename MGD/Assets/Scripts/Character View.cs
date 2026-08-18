using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;

public class CharacterView : MonoBehaviour
{

    [Serializable]
    public class CharacterSlot
    {
            public string slotID;
            public Image portraitImage;
            public Transform transform;
            public string currentCharacterName;
            public Tween activeTween;

    }


    [Header("Characters")]
    public List<CharacterSlot> slots;

    [Header("Character Database")]
    public List<CharacterData> characterDatabase;

    [Header("Animation Settings")]
    public float fadeDuration = 0.2f;
    public float inactiveAlpha = 0.4f;
    public float activeScale = 1.05f;
    public float inactiveScale = 1.0f;

    private Dictionary<string, CharacterData> characterLookup;


    //Loads all the character data.
    void Awake()
    {
        characterLookup = new();
        foreach (var charData in characterDatabase)
        {
            if(charData != null && !characterLookup.ContainsKey(charData.characterName)) //check if there's valid character data, then check if it already exists in the dictionary
            {
                characterLookup.Add(charData.characterName, charData);
            }
        }
    }

    private CharacterSlot GetSlot(string slotID)
    {
        return slots.Find(slot => slot.slotID.Equals(slotID, StringComparison.OrdinalIgnoreCase));
    }

    public void SetCharacter(string characterName, string slotID, string expression = "default")
    {
        CharacterSlot targetSlot = GetSlot(slotID);
    }



}
