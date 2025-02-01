using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WASD : MonoBehaviour
{
    [Header("Tutorial Settings")]
    [TextArea]
    public string tutorialMessage; // Mensagem de tutorial para exibir

    [Header("UI Reference")]
    public TextMeshProUGUI tutorialText; // Referência ao tutorial
    [SerializeField] private Image interactionIcon; // Ícone da tecla "E"
    [SerializeField] private Transform iconPosition; // Posição manual do ícone

    private bool jaAtivado = false; // Tutorial só aparece uma vez
    private bool isPlayerNearby = false; // Verifica se o player está perto
    private Coroutine currentCoroutine = null;

    private void Start()
    {
        if (interactionIcon != null)
            interactionIcon.gameObject.SetActive(false); // O ícone começa desativado
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!jaAtivado)
            {
                DisplayMessage();
                jaAtivado = true; // Marca que o tutorial foi ativado
            }

            ShowInteractionIcon();
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideInteractionIcon();
            isPlayerNearby = false;
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interação iniciada!");
            // Aqui você pode chamar o diálogo ou qualquer outra ação
        }
    }

    private void DisplayMessage()
    {
        if (tutorialText != null)
        {
            tutorialText.text = tutorialMessage;
            tutorialText.gameObject.SetActive(true);
            if (currentCoroutine != null)
                StopCoroutine(currentCoroutine);

            // Inicia uma nova contagem regressiva
            currentCoroutine = StartCoroutine(HideMessageAfterTime(5f)); // Oculta o tutorial após 3 segundos
        }
    }

    private IEnumerator HideMessageAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (tutorialText != null)
            tutorialText.gameObject.SetActive(false);
    }

    private void ShowInteractionIcon()
    {
        if (interactionIcon != null && iconPosition != null)
        {
            interactionIcon.gameObject.SetActive(true);
            interactionIcon.transform.position = Camera.main.WorldToScreenPoint(iconPosition.position); // Converte posição
        }
    }

    private void HideInteractionIcon()
    {
        if (interactionIcon != null)
        {
            interactionIcon.gameObject.SetActive(false);
        }
    }
}
