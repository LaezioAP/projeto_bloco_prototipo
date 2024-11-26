using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObject/Dialogue")]
public class DialogueDataSO : ScriptableObject
{
    public List<DialogueSentence> Sentences;

}

[Serializable]
public class DialogueSentence 
{
    public CharacterDataSO ActorData;
    [TextArea(3, 5)]
    public string content;
}
