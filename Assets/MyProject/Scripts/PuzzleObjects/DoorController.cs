using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 5f; // Velocidade da rotação da porta
    [SerializeField] float autoCloseTime = 3f; // Tempo até a porta fechar automaticamente
    private bool isOpen = false; // Estado da porta
    private bool playerInRange = false; // Verifica se o player está próximo
    private Quaternion targetRotation; // Guarda a rotação alvo da porta
    private Transform player; // Referência ao jogador

    [Header("UI Elements")]
    [SerializeField] private Image interactionIcon; // Ícone da tecla "E"
    [SerializeField] private Transform iconPosition; // Posição manual do ícone

    [Header("Audio Settings")]
    [SerializeField] private AudioSource doorAudioSource; // ?? Componente de som da porta
    [SerializeField] private AudioClip openSound; // ?? Som ao abrir a porta
    [SerializeField] private AudioClip closeSound; // ?? Som ao fechar a porta

    void Start()
    {
        // Define a posição inicial da porta como fechada
        targetRotation = transform.rotation;

        if (interactionIcon != null)
            interactionIcon.gameObject.SetActive(false);
    }

    void Update()
    {
        // Suaviza a rotação da porta para o ângulo desejado
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // Se o jogador estiver perto e pressionar "E", abre a porta
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isOpen)
        {
            OpenDoor();
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

    private void OpenDoor()
    {
        isOpen = true;

        // Calcula a direção do player em relação à porta
        Vector3 direction = player.position - transform.position;

        // Define o ângulo de abertura com base na posição do player
        float angle = Vector3.Dot(transform.right, direction) > 0 ? 90f : -90f;
        targetRotation = Quaternion.Euler(0, 0, angle); // Define o ângulo aberto

        // ?? Toca o som de abrir porta
        if (doorAudioSource != null && openSound != null)
            doorAudioSource.PlayOneShot(openSound);

        // Agendar o fechamento automático da porta após "autoCloseTime" segundos
        Invoke("CloseDoor", autoCloseTime);
    }

    private void CloseDoor()
    {
        isOpen = false;
        targetRotation = Quaternion.Euler(0, 0, 0); // Define o ângulo fechado

        // ?? Toca o som de fechar porta
        if (doorAudioSource != null && closeSound != null)
            doorAudioSource.PlayOneShot(closeSound);
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
