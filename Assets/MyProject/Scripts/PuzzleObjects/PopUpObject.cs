using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpObject : MonoBehaviour
{
    [SerializeField] private PopUpWindow popUpWindow;
    private bool canShowPopup = false;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canShowPopup)
        {
            Debug.Log("Player entrou e pop-up ativado!");
            
        }
    }

    public void TriggerPopUp()
    {
        canShowPopup = true;
        gameObject.SetActive(true);
        OpenConfirmationWindow("Usar a chave?");
    }

    private void OpenConfirmationWindow(string message)
    {
        popUpWindow.gameObject.SetActive(true);
        popUpWindow.yesButton.onClick.RemoveAllListeners();
        popUpWindow.noButton.onClick.RemoveAllListeners();
        popUpWindow.yesButton.onClick.AddListener(YesClicked);
        popUpWindow.noButton.onClick.AddListener(NoClicked);
        popUpWindow.messageText.text = message;
    }

    private void YesClicked()
    {
        popUpWindow.gameObject.SetActive(false);
        Debug.Log("Sim clicado");
    }

    private void NoClicked()
    {
        popUpWindow.gameObject.SetActive(false);
        Debug.Log("Não clicado");
    }
}
