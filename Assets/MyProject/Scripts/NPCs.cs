using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCs : MonoBehaviour
{
    [SerializeField] private DialogueDataSO dialogueData;
    private bool isPlayerNearby = false;
    [SerializeField] private Image interactionIcon; // �cone da tecla "E"
    [SerializeField] private Transform iconPosition; // Posi��o manual do �cone


    protected virtual void Start()
    {
        if (interactionIcon != null)
            interactionIcon.gameObject.SetActive(false); // O �cone come�a desativado
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player pr�ximo");
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
            Debug.Log("Di�logo iniciado!");
            GameEvents.Instance.StartDialogue(dialogueData);
        }
    }

    protected virtual void OnPlayerInteract()
    {
        // Este m�todo pode ser sobrescrito nas classes filhas
        Debug.Log("Intera��o padr�o com NPC.");
    }

    private void ShowInteractionIcon()
    {
        if (interactionIcon != null && iconPosition != null)
        {
            interactionIcon.gameObject.SetActive(true);
            interactionIcon.transform.position = Camera.main.WorldToScreenPoint(iconPosition.position); // Converte posi��o
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
