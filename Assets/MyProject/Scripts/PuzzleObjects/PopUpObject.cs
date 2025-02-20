using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PopUpObject : MonoBehaviour
{
    [SerializeField] private PopUpWindow popUpWindow;
    private bool canShowPopup = false;
    private Action onYesClicked;
    private Action onNoClicked;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canShowPopup)
        {
            Debug.Log("Player entrou e pop-up ativado!");
        }
    }

    public void TriggerPopUp(string message, Action yesAction, Action noAction)
    {
        canShowPopup = true;
        gameObject.SetActive(true);
        OpenConfirmationWindow(message, yesAction, noAction);
    }

    private void OpenConfirmationWindow(string message, Action yesAction, Action noAction)
    {
        popUpWindow.gameObject.SetActive(true);
        popUpWindow.messageText.text = message;

        popUpWindow.yesButton.onClick.RemoveAllListeners();
        popUpWindow.noButton.onClick.RemoveAllListeners();

        onYesClicked = yesAction;
        onNoClicked = noAction;

        popUpWindow.yesButton.onClick.AddListener(() => YesClicked());
        popUpWindow.noButton.onClick.AddListener(() => NoClicked());
    }

    private void YesClicked()
    {
        popUpWindow.gameObject.SetActive(false);
        Debug.Log("Sim clicado");
        onYesClicked?.Invoke();
    }

    private void NoClicked()
    {
        popUpWindow.gameObject.SetActive(false);
        Debug.Log("Não clicado");
        onNoClicked?.Invoke();
    }
}
