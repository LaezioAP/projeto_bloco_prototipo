using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Image charImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private DialogueBar dialogueBar;
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
            yield return new WaitForSeconds(intervalBetweenSentences); 
        }
        yield return dialogueBar.HiddeBar();
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnStartDialogue -= HandleStartDialogue;
    }
    void Update()
    {
        
    }
}
