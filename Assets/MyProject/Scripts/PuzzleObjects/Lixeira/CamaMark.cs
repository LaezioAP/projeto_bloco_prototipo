using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CamaMark : MonoBehaviour
{
    [SerializeField] private PopUpObject popUpObject;
    [SerializeField] private InventorySO playerInventory;
    [SerializeField] private ItemSO recompensa; // Item que o jogador ganha ao vasculhar
    [SerializeField] private int quantidadeRecompensa = 1;
    [SerializeField] private DialogueDataSO posRecompensa; // Diálogo após pegar a recompensa
    [SerializeField] private DialogueDataSO interacaoPosRecompensa; // Diálogo para interações futuras
    [SerializeField] private DialogueDataSO dialogoRecusa; // Diálogo ao recusar
    [SerializeField] private Image interactionIcon; // Ícone da tecla "E"
    [SerializeField] private Transform iconPosition; // Posição manual do ícone


    private bool popUpAtivado = false;
    private bool camaVasculhada = false; // Impede o pop-up após vasculhar
    private bool jogadorProximo = false; // Verifica se o jogador está no alcance
    private bool inputCooldown = false; // Evita múltiplos disparos acidentais

    // Detecta quando o jogador entra no alcance da lixeira

    private void Start()
    {
        if (interactionIcon != null)
            interactionIcon.gameObject.SetActive(false); // O ícone começa desativado
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowInteractionIcon();
            jogadorProximo = true;
        }
    }

    // Detecta quando o jogador sai do alcance
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorProximo = false;
            HideInteractionIcon();
            if (popUpAtivado)
            {
                ClosePopUp(); // Fecha o pop-up se o jogador sair do alcance
            }
        }
    }

    private void Update()
    {
        // Verifica a interação inicial com a lixeira
        if (jogadorProximo && !camaVasculhada && !popUpAtivado && !inputCooldown)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                MostrarPopUp();
                StartCoroutine(InputCooldown());
            }
        }

        // Verifica interação após vasculhar
        if (jogadorProximo && camaVasculhada && !inputCooldown)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameEvents.Instance.StartDialogue(interacaoPosRecompensa);
                StartCoroutine(InputCooldown());
            }
        }
    }

    private void MostrarPopUp()
    {
        popUpAtivado = true;
        popUpObject.TriggerPopUp("Bagunçar a cama do Mark?",
            () => VasculharCama(), // Ação do "Sim"
            () => RecusarVasculhar()  // Ação do "Não"
        );
    }

    private void VasculharCama()
    {
        playerInventory.AddItem(recompensa, quantidadeRecompensa);
        camaVasculhada = true;
        GameEvents.Instance.StartDialogue(posRecompensa);
        ClosePopUp();
    }

    private void RecusarVasculhar()
    {
        GameEvents.Instance.StartDialogue(dialogoRecusa);
        ClosePopUp();
    }

    private void ClosePopUp()
    {
        popUpAtivado = false;

    }

    // Pequeno cooldown para evitar múltiplos disparos acidentais
    private IEnumerator InputCooldown()
    {
        inputCooldown = true;
        yield return new WaitForSeconds(0.2f); // 200ms de espera
        inputCooldown = false;
    }
}