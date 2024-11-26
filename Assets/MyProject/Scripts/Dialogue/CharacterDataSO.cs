using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Character")]
public class CharacterDataSO : ScriptableObject
{
    public string CharName;
    public Sprite sprite;
}
