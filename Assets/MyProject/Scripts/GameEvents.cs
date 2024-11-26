using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(-1)]
public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public event Action<DialogueDataSO> OnStartDialogue;

    public void StartDialogue(DialogueDataSO dialogueDataSO) => OnStartDialogue?.Invoke(dialogueDataSO);

    public event Action OnFinishDialogue;

    public void FinishDialogue() => OnFinishDialogue?.Invoke();
}
