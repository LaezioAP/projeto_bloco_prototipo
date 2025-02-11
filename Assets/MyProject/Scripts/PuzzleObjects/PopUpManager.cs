using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private GameObject popUp; // O pop-up de pergunta
    [SerializeField] private Button btnSim; // Bot�o "Sim"
    [SerializeField] private Button btnNao; // Bot�o "N�o"

    private System.Action onSimCallback; // A��o a ser executada quando a resposta for "Sim"
    private System.Action onNaoCallback; // A��o a ser executada quando a resposta for "N�o"

    private void Start()
    {
        // Adiciona os listeners aos bot�es
        btnSim.onClick.AddListener(RespostaSim);
        btnNao.onClick.AddListener(RespostaNao);

        // Inicialmente desativa o pop-up
        popUp.SetActive(false);
    }

    // Exibe o pop-up
    public void ShowPopUp(System.Action simCallback, System.Action naoCallback)
    {
        onSimCallback = simCallback;
        onNaoCallback = naoCallback;
        popUp.SetActive(true); // Exibe o pop-up
    }

    // Fun��o chamada se o player responder "Sim"
    private void RespostaSim()
    {
        onSimCallback?.Invoke(); // Executa a a��o do "Sim"
        popUp.SetActive(false); // Fecha o pop-up
    }

    // Fun��o chamada se o player responder "N�o"
    private void RespostaNao()
    {
        onNaoCallback?.Invoke(); // Executa a a��o do "N�o"
        popUp.SetActive(false); // Fecha o pop-up
    }
}
