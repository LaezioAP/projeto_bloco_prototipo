using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armarinho : NPCs
{
    [SerializeField] private PopUpManager popUpManager; // Refer�ncia ao PopUpManager

    /*protected override void Start() 
    {
        base.Start(); // Chama o Start da classe base
    }

    protected override void OnPlayerInteract()
    {
        base.OnPlayerInteract(); // Chama a implementa��o padr�o de intera��o (opcional)

        // Aqui voc� pode adicionar a l�gica de verifica��o se o jogador tem a chave
        popUpManager.ShowPopUp(RespostaSim, RespostaNao);
    }

    /*private void RespostaSim()
    {
        if (PlayerHasKey())
        {
            Debug.Log("Voc� tem a chave! O arm�rio est� aberto.");
            // Aqui voc� pode adicionar a l�gica para abrir o arm�rio ou o que for necess�rio
        }
        else
        {
            Debug.Log("Voc� n�o tem a chave.");
        }
    }

    private void RespostaNao()
    {
        Debug.Log("Voc� n�o respondeu corretamente.");
    }

    /* Verifica se o jogador tem a chave (adapte para o seu sistema de invent�rio)
    bool PlayerHasKey()
    {
        return PlayerInventory.HasItem("Chave");
    }*/
}
