using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpObject : MonoBehaviour
{
    [SerializeField]private PopUpWindow popUpWindow;
    // Start is called before the first frame update
    void Start()
    {
        OpenConfirmationWindow("Usar a chave?");
    }

    private void OpenConfirmationWindow(string message) 
    {
        popUpWindow.gameObject.SetActive(true);
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
