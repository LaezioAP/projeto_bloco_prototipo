using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gravador : MonoBehaviour
{
    [SerializeField] TMP_InputField[] numberSlots; // Array de 4 TMP_InputFields
    [SerializeField] Button confirmButton;         // Botão de confirmação
    [SerializeField] TMP_Text feedbackText;        // Texto de feedback (TextMeshPro)
    [SerializeField] private Image interactionIcon; // Ícone da tecla "E"
    [SerializeField] private Transform iconPosition; // Posição manual do ícone
    [SerializeField] private GameObject panel;     // Referência ao painel do gravador
    [SerializeField] private DialogueDataSO dialogueSecret;
    [SerializeField] private DialogueDataSO dialogueBlocked;

    private string correctCode = "2519"; // Código correto
    private int attemptsLeft = 3;        // Tentativas restantes
    private bool isPlayerInRange = false; // Verifica se o jogador está no trigger

    void Start()
    {
        // Configura os eventos de mudança de valor para cada InputField
        for (int i = 0; i < numberSlots.Length; i++)
        {
            int index = i; // Captura o índice atual para a lambda
            numberSlots[i].onValueChanged.AddListener(delegate { OnInputValueChanged(index); });
        }

        if (confirmButton != null)
            confirmButton.onClick.AddListener(CheckCode); 
        if (interactionIcon != null)
            interactionIcon.gameObject.SetActive(false);
        if (panel != null)
            panel.SetActive(false); // Desativa o painel por padrão
    }

    void Update()
    {
        // Só verifica a tecla "E" se o jogador estiver no trigger
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TogglePanel();
            if (attemptsLeft == 0)
            {
                feedbackText.text = $"Gravador Bloqueado!";
            }
            else
            {
                feedbackText.text = "Digite a senha para ouvir a mensagem:";
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            ShowInteractionIcon();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            HideInteractionIcon();
            if (panel != null)
                panel.SetActive(false); // Desativa o painel ao sair do trigger
        }
    }

    void OnInputValueChanged(int currentIndex)
    {
        // Se um dígito foi digitado
        if (numberSlots[currentIndex].text.Length == 1)
        {
            // Move o foco para o próximo campo, se não for o último
            if (currentIndex < numberSlots.Length - 1)
            {
                numberSlots[currentIndex + 1].Select();
            }
            // Se for o último campo, verifica o código
            else if (currentIndex == numberSlots.Length - 1)
            {
                CheckCode();
            }
        }
    }

    void CheckCode()
    {
        string playerInput = "";
        foreach (TMP_InputField slot in numberSlots)
        {
            playerInput += slot.text;
        }

        if (playerInput == correctCode)
        {
            feedbackText.text = "Correto! Conversa secreta liberada.";
            UnlockSecretConversation();
        }
        else
        {
            attemptsLeft--;
            if (attemptsLeft > 0)
            {
                feedbackText.text = $"Errado! Tentativas restantes: {attemptsLeft}";
                ResetInputFields();
            }
            else
            {
                feedbackText.text = "Gravador bloqueado!";
                LockRecorder();
            }
        }
    }

    void UnlockSecretConversation()
    {
        GameEvents.Instance.StartDialogue(dialogueSecret);
        
        if (panel != null)
            panel.SetActive(false);
    }

    void LockRecorder()
    {
        foreach (TMP_InputField slot in numberSlots)
        {
            slot.interactable = false;
        }
        confirmButton.interactable = false;
        if (panel != null)
            panel.SetActive(false); // Fecha o painel ao bloquear
        Debug.Log("Gravador bloqueado!");
        GameEvents.Instance.StartDialogue(dialogueBlocked);
    }

    void ResetInputFields()
    {
        foreach (TMP_InputField slot in numberSlots)
        {
            slot.text = ""; // Limpa o texto
        }
        numberSlots[0].Select(); // Volta o foco para o primeiro campo
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
        {
            interactionIcon.gameObject.SetActive(false);
        }
    }

    private void TogglePanel()
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf);
            if (panel.activeSelf)
            {
                HideInteractionIcon();
                numberSlots[0].Select(); // Foca no primeiro campo ao abrir o painel
                ResetInputFields(); // Limpa os campos ao abrir (opcional)
            }
            else
            {
                ShowInteractionIcon();
            }
        }
    }
}