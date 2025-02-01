using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventTutorial : MonoBehaviour
{
    [Header("Tutorial Settings")]
    [TextArea]
    public string tutorialMessage; // Mensagem do tutorial
    public TextMeshProUGUI tutorialText; // Referência ao TextMeshProUGUI no Canvas

    private static bool messageDisplayed = false; // Impede que a mensagem seja exibida mais de uma vez
    private Coroutine currentCoroutine = null; // Guarda a referência da contagem regressiva

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!messageDisplayed && other.CompareTag("Interavel"))
        {
            DisplayMessage();
            messageDisplayed = true; // Impede que a mensagem seja exibida novamente
        }
    }

    private void DisplayMessage()
    {
        if (tutorialText != null)
        {
            tutorialText.text = tutorialMessage;
            tutorialText.gameObject.SetActive(true);

            // Se já houver uma contagem regressiva ativa, cancela antes de iniciar outra
            if (currentCoroutine != null)
                StopCoroutine(currentCoroutine);

            // Inicia uma nova contagem regressiva
            currentCoroutine = StartCoroutine(HideMessageAfterTime(3f));
        }
    }

    private IEnumerator HideMessageAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (tutorialText != null)
        {
            tutorialText.gameObject.SetActive(false);
        }
    }
}
