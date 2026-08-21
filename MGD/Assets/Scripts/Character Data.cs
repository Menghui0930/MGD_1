using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
        public string characterName;
        public List<Expression> expressions;


    [System.Serializable]
    public class Expression
    {
        public string expressionName;
        public Sprite sprite;
    }

    public Sprite GetSprite(string expressionName)
    {
     var newExpression = expressions.Find(e => e.expressionName.Equals(expressionName,StringComparison.OrdinalIgnoreCase));
     if (newExpression != null) return newExpression.sprite;

    if (expressions.Count > 0)
        return expressions[0].sprite; //fallback to first sprite

    else return null;  
    }
    
}
