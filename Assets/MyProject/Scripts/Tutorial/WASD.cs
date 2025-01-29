using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WASD : MonoBehaviour
{
    [Header("Tutorial Settings")]
    [TextArea]
    public string tutorialMessage; // Mensagem de tutorial para exibir
    public float displayTime = 3f; // Tempo que a mensagem ficará na tela

    [Header("UI Reference")]
    public TextMeshProUGUI tutorialText; // Referência ao TextMeshProUGUI no canvas

    private float timer = 0f; // Temporizador interno
    private bool isDisplaying = false; // Verifica se a mensagem está ativa
    private bool jaAtivado = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!jaAtivado && other.CompareTag("Player")) // Verifica se o objeto é o player
        {
            DisplayMessage();
        }
    }

    private void Update()
    {
        if (isDisplaying)
        {
            timer += Time.deltaTime;
            if (timer >= displayTime)
            {
                HideMessage();
            }
        }
    }

    private void DisplayMessage()
    {
        if (tutorialText != null)
        {
            tutorialText.text = tutorialMessage;
            tutorialText.gameObject.SetActive(true);
            isDisplaying = true;
            jaAtivado = true;
        }
    }

    private void HideMessage()
    {
        if (tutorialText != null)
        {
            tutorialText.gameObject.SetActive(false);
            isDisplaying = false;
            timer = 0f;
        }
    }
}