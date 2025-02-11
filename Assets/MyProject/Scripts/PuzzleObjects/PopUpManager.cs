using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private GameObject popUp; // O pop-up de pergunta
    [SerializeField] private Button btnSim; // Botão "Sim"
    [SerializeField] private Button btnNao; // Botão "Não"

    private System.Action onSimCallback; // Ação a ser executada quando a resposta for "Sim"
    private System.Action onNaoCallback; // Ação a ser executada quando a resposta for "Não"

    private void Start()
    {
        // Adiciona os listeners aos botões
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

    // Função chamada se o player responder "Sim"
    private void RespostaSim()
    {
        onSimCallback?.Invoke(); // Executa a ação do "Sim"
        popUp.SetActive(false); // Fecha o pop-up
    }

    // Função chamada se o player responder "Não"
    private void RespostaNao()
    {
        onNaoCallback?.Invoke(); // Executa a ação do "Não"
        popUp.SetActive(false); // Fecha o pop-up
    }
}
