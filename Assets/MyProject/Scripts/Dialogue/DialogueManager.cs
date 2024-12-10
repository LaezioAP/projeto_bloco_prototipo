using System.Collections;
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

    private bool skipToNextSentence = false; // Controle para pular sentença
    private bool animationSkipped = false; // Controle para pular animação
    private bool waitingForInput = false;  // Controle para esperar input do jogador

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

            // Exibir texto com animação
            yield return StartCoroutine(ShowSentence(sentence.content));

            // Aguardar input para continuar para a próxima sentença
            waitingForInput = true;
            while (waitingForInput)
            {
                yield return null; // Espera até o jogador pressionar espaço
            }
        }

        messageText.HideText();
        yield return dialogueBar.HiddeBar();
        GameEvents.Instance.FinishDialogue();
    }

    private IEnumerator ShowSentence(string content)
    {
        animationSkipped = false;

        // Inicia a animação do texto
        yield return messageText.ShowText(content);

        // Marca como animação concluída
        animationSkipped = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!animationSkipped)
            {
                // Pular a animação do texto
                messageText.SkipAnimation();
                animationSkipped = true;
            }
            else if (waitingForInput)
            {
                // Avançar para a próxima sentença
                waitingForInput = false;
            }
        }
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnStartDialogue -= HandleStartDialogue;
    }
}
