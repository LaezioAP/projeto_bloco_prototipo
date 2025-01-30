using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WASD : MonoBehaviour
{
    [Header("Tutorial Settings")]
    [TextArea]
    public string tutorialMessage; // Mensagem de tutorial para exibir
     // Tempo que a mensagem ficará na tela (não será mais usado)

    [Header("UI Reference")]
    public TextMeshProUGUI tutorialText; // Referência ao TextMeshProUGUI no canvas

    private bool jaAtivado = false; // Garante que a mensagem aparece apenas uma vez

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!jaAtivado && other.CompareTag("Player")) // Verifica se o objeto é o player
        {
            DisplayMessage();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (jaAtivado && other.CompareTag("Player")) // Esconde a mensagem ao sair do trigger
        {
            HideMessage();
        }
    }

    private void DisplayMessage()
    {
        if (tutorialText != null)
        {
            tutorialText.text = tutorialMessage;
            tutorialText.gameObject.SetActive(true);
            jaAtivado = true;
        }
    }

    private void HideMessage()
    {
        if (tutorialText != null)
        {
            tutorialText.gameObject.SetActive(false);
        }
    }
}