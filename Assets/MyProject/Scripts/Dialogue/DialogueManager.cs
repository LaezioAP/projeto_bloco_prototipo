using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image charImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private DialogueBar dialogueBar;
    [SerializeField] private MessageText messageText;

    [Header("Settings")]
    [SerializeField] private float intervalBetweenSentences = 1;
    void Start()
    {
        GameEvents.Instance.OnStartDialogue += HandleStartDialogue;
    }

    private void HandleStartDialogue(DialogueDataSO dialogueData)
    {
        StartCoroutine(StartDialogue(dialogueData));
    }

    private IEnumerator StartDialogue(DialogueDataSO dialogueDataSO) 
    {
        charImage.enabled = false;
        nameText.SetText("");

        yield return dialogueBar.ShowBar();
        charImage.enabled = true;

        foreach (var sentence in dialogueDataSO.Sentences) 
        {
            nameText.SetText(sentence.ActorData.CharName);
            charImage.sprite = sentence.ActorData.sprite;
            yield return messageText.ShowText(sentence.content);
            yield return new WaitForSeconds(intervalBetweenSentences); 
        }
        messageText.HideText();
        yield return dialogueBar.HiddeBar();
        GameEvents.Instance.FinishDialogue();
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnStartDialogue -= HandleStartDialogue;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) messageText.SkipAnimation();
    }
}
