using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCs : MonoBehaviour
{
    [SerializeField] private DialogueDataSO dialogueData;
    private bool isPlayerNearby = false;
    [SerializeField] private Image interactionIcon; // Ícone da tecla "E"
    [SerializeField] private Transform iconPosition; // Posição manual do ícone


    protected virtual void Start()
    {
        if (interactionIcon != null)
            interactionIcon.gameObject.SetActive(false); // O ícone começa desativado
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player próximo");
            ShowInteractionIcon();
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideInteractionIcon();
            Debug.Log("Player saiu");
            isPlayerNearby = false;
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Diálogo iniciado!");
            GameEvents.Instance.StartDialogue(dialogueData);
        }
    }

    protected virtual void OnPlayerInteract()
    {
        // Este método pode ser sobrescrito nas classes filhas
        Debug.Log("Interação padrão com NPC.");
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
