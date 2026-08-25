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
        foreach (var slot in slots)
        {
            if (slot.portraitImage != null)
            {
                // Set initial alpha to 0 and disable rendering
                Color color = slot.portraitImage.color;
                color.a = 0f;
                slot.portraitImage.color = color;
                
                slot.portraitImage.enabled = false;
                slot.portraitImage.raycastTarget = false;
            }
        }
        
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
        CharacterSlot targetSlot = GetSlot(slotID); //checks if the slot exists
        if (targetSlot == null) return;

        if(!characterLookup.TryGetValue(characterName, out CharacterData data)) return; //check if the character data exists


        Sprite newSprite = data.GetSprite(expression);
        if(newSprite != null)
        {
            targetSlot.portraitImage.sprite = newSprite;
            targetSlot.currentCharacterName = characterName;

            //Turns the Image on
            targetSlot.portraitImage.enabled = true;
        }
    }

    public void ClearSlot(string slotId)
    {
        CharacterSlot slot = GetSlot(slotId);
        if (slot == null) return;

        slot.activeTween?.Kill();
        
        slot.activeTween = slot.portraitImage.DOFade(0f, fadeDuration)
            .OnComplete(() => {
                slot.currentCharacterName = string.Empty;
                slot.portraitImage.sprite = null;
                slot.portraitImage.enabled = false;
            });
    }

    public void HighlightSpeaker(string activeCharacterName) //fades nonactive character and unfades the active character
    {
        foreach (var slot in slots)
        {
            bool isActiveSpeaker = slot.currentCharacterName.Equals(activeCharacterName, StringComparison.OrdinalIgnoreCase);
            
            slot.activeTween?.Kill();

            float targetAlpha = isActiveSpeaker? 1.0f: inactiveAlpha;
            float targetScale = isActiveSpeaker? activeScale: inactiveScale;

            Sequence animationSequence = DOTween.Sequence();
            animationSequence.Join(slot.portraitImage.DOFade(targetAlpha, fadeDuration));
            animationSequence.Join(slot.portraitImage.transform.DOScale(targetScale, fadeDuration)).SetEase(Ease.OutQuad);
        }
    }



}
