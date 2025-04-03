using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PoraoInteraction : MonoBehaviour
{
    public string sceneToLoad = "NextScene"; // Nome da cena a ser carregada
    private bool playerInRange = false;
    [Header("UI Elements")]
    [SerializeField] private Image interactionIcon; // Ícone da tecla "E"
    [SerializeField] private Transform iconPosition; // Posição manual do ícone


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange) 
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o colisor é o player e se a tecla E foi pressionada
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            ShowInteractionIcon();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HideInteractionIcon();
        }
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