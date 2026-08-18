using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
        public string characterName;
        public List<CharacterSprite> characterSprite;


    [System.Serializable]
    public class CharacterSprite
    {
        public string spriteName;
        public Sprite sprite;
    }
}
