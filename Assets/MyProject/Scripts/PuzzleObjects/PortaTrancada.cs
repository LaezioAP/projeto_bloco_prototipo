using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortaTrancadaController : MonoBehaviour
{
    [SerializeField] private PopUpObject popUpObject;
    [SerializeField] private InventorySO playerInventory;
    [SerializeField] private ItemSO chave; // Item da chave no inventário
    [SerializeField] private DialogueDataSO dialogoSemChave; // Diálogo para quando não tem a chave
    [SerializeField] private DialogueDataSO dialogoPortaAberta; // Diálogo para quando a porta é aberta pela primeira vez

    [Header("Door Settings")]
    [SerializeField] private float rotationSpeed = 5f; // Velocidade da rotação da porta
    [SerializeField] private float autoCloseTime = 3f; // Tempo até a porta fechar automaticamente
    private bool isOpen = false; // Estado atual da porta (aberta ou fechada)
    private bool isUnlocked = false; // Indica se a porta foi destrancada permanentemente
    private bool playerInRange = false; // Verifica se o player está próximo
    private bool popUpAtivado = false; // Controla o estado do pop-up
    private Quaternion targetRotation; // Guarda a rotação alvo da porta
    private Transform player; // Referência ao jogador

    [Header("UI Elements")]
    [SerializeField] private Image interactionIcon; // Ícone da tecla "E"
    [SerializeField] private Transform iconPosition; // Posição manual do ícone

    [Header("Audio Settings")]
    [SerializeField] private AudioSource doorAudioSource; // Componente de som da porta
    [SerializeField] private AudioClip openSound; // Som ao abrir a porta
    [SerializeField] private AudioClip closeSound; // Som ao fechar a porta
    [SerializeField] private AudioClip lockedSound; // Som ao tentar abrir sem chave

    void Start()
    {
        targetRotation = transform.rotation; // Define a posição inicial da porta como fechada
        if (interactionIcon != null)
            interactionIcon.gameObject.SetActive(false);
    }

    void Update()
    {
        // Suaviza a rotação da porta para o ângulo desejado
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // Se o jogador estiver perto e pressionar "E", interage com a porta
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (isUnlocked && !isOpen) // Se já estiver destrancada e fechada, abre normalmente
            {
                OpenDoor();
            }
            else if (!isUnlocked && !isOpen) // Se ainda estiver trancada, verifica a chave
            {
                InteragirComPorta();
            }
            else if (isOpen) // Se estiver aberta, fecha
            {
                CloseDoor();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true; // O jogador está próximo da porta
            player = collision.transform; // Armazena a referência ao player
            ShowInteractionIcon();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false; // O jogador saiu da área da porta
            player = null;
            HideInteractionIcon();
        }
    }

    public void InteragirComPorta()
    {
        if (isOpen || isUnlocked) return; // Se a porta já estiver aberta ou destrancada, não faz nada

        if (!popUpAtivado && popUpObject != null)
        {
            int chaveIndex = EncontrarItemNoInventario(chave);

            if (chaveIndex != -1) // Se o jogador tem a chave
            {
                popUpObject.TriggerPopUp("Usar a chave?",
                    () => AbrirPorta(chaveIndex),
                    () => { popUpAtivado = false; Debug.Log("O jogador recusou usar a chave."); });
            }
            else // Se o jogador não tem a chave
            {
                // Toca o som de porta trancada antes de abrir o pop-up
                if (doorAudioSource != null && lockedSound != null)
                    doorAudioSource.PlayOneShot(lockedSound);

                popUpObject.TriggerPopUp("A porta está trancada. Você não tem a chave.",
                    () => MostrarDialogoSemChave(),
                    () => { popUpAtivado = false; Debug.Log("O jogador fechou o pop-up."); });
            }

            popUpAtivado = true;
        }
    }

    private void AbrirPorta(int chaveIndex)
    {
     
        playerInventory.RemoveItem(chaveIndex, 1); // Remove a chave do inventário
        isOpen = true;
        isUnlocked = true; // Marca a porta como destrancada permanentemente

        // Calcula a direção do player em relação à porta, se o player estiver definido
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            float angle = Vector3.Dot(transform.right, direction) > 0 ? 90f : -90f;
            targetRotation = Quaternion.Euler(0, 0, angle); // Define o ângulo aberto
        }
        else
        {
            targetRotation = Quaternion.Euler(0, 0, 90f); // Direção padrão
            Debug.LogWarning("Player não detectado, abrindo a porta em direção padrão.");
        }

        // Toca o som de abrir porta
        if (doorAudioSource != null && openSound != null)
            doorAudioSource.PlayOneShot(openSound);

        // Inicia o diálogo de porta aberta, se configurado
        if (GameEvents.Instance != null && dialogoPortaAberta != null)
            GameEvents.Instance.StartDialogue(dialogoPortaAberta);

        Invoke("CloseDoor", autoCloseTime); // Fecha a porta automaticamente após um tempo
    }

    private void OpenDoor() // Método para abrir a porta normalmente após destrancar
    {
        isOpen = true;

        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            float angle = Vector3.Dot(transform.right, direction) > 0 ? 90f : -90f;
            targetRotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            targetRotation = Quaternion.Euler(0, 0, 90f);
        }

        if (doorAudioSource != null && openSound != null)
            doorAudioSource.PlayOneShot(openSound);

        Invoke("CloseDoor", autoCloseTime); // Fecha automaticamente
    }

    private void CloseDoor()
    {
        isOpen = false;
        targetRotation = Quaternion.Euler(0, 0, 0); // Define o ângulo fechado

        if (doorAudioSource != null && closeSound != null)
            doorAudioSource.PlayOneShot(closeSound);
    }

    private void MostrarDialogoSemChave()
    {
        if (GameEvents.Instance != null && dialogoSemChave != null)
            GameEvents.Instance.StartDialogue(dialogoSemChave);
        popUpAtivado = false; // Reseta o pop-up
    }

    private int EncontrarItemNoInventario(ItemSO item)
    {
        if (playerInventory == null) return -1;
        var inventario = playerInventory.GetCurrentInventoryState();

        foreach (var slot in inventario)
        {
            if (slot.Value.item == item)
            {
                return slot.Key; // Retorna o índice do item no inventário
            }
        }
        return -1; // Retorna -1 caso o item não seja encontrado
    }

    private void ShowInteractionIcon()
    {
        if (interactionIcon != null && iconPosition != null)
        {
            interactionIcon.gameObject.SetActive(true);
            interactionIcon.transform.position = Camera.main.WorldToScreenPoint(iconPosition.position);
        }
    }

    private void HideInteractionIcon()
    {
        if (interactionIcon != null)
            interactionIcon.gameObject.SetActive(false);
    }

    private void OnDialogueEnded()
    {
        popUpAtivado = false; // Reseta o pop-up quando o diálogo termina
    }

    private void OnEnable()
    {
        if (GameEvents.Instance != null)
            GameEvents.Instance.OnFinishDialogue += OnDialogueEnded;
    }

    private void OnDisable()
    {
        if (GameEvents.Instance != null)
            GameEvents.Instance.OnFinishDialogue -= OnDialogueEnded;
    }
}