using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armarinho : NPCs
{
    [SerializeField] private PopUpManager popUpManager; // Referência ao PopUpManager

    /*protected override void Start() 
    {
        base.Start(); // Chama o Start da classe base
    }

    protected override void OnPlayerInteract()
    {
        base.OnPlayerInteract(); // Chama a implementação padrão de interação (opcional)

        // Aqui você pode adicionar a lógica de verificação se o jogador tem a chave
        popUpManager.ShowPopUp(RespostaSim, RespostaNao);
    }

    /*private void RespostaSim()
    {
        if (PlayerHasKey())
        {
            Debug.Log("Você tem a chave! O armário está aberto.");
            // Aqui você pode adicionar a lógica para abrir o armário ou o que for necessário
        }
        else
        {
            Debug.Log("Você não tem a chave.");
        }
    }

    private void RespostaNao()
    {
        Debug.Log("Você não respondeu corretamente.");
    }

    /* Verifica se o jogador tem a chave (adapte para o seu sistema de inventário)
    bool PlayerHasKey()
    {
        return PlayerInventory.HasItem("Chave");
    }*/
}
