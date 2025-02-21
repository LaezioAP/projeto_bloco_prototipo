using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armarinho : NPCs
{
    [SerializeField] private PopUpObject popUpObject;
    [SerializeField] private InventorySO playerInventory;
    [SerializeField] private ItemSO chaveArmarinho;
    [SerializeField] private ItemSO recompensa;
    [SerializeField] private int quantidadeRecompensa = 1;
    [SerializeField] private DialogueDataSO posRecompensa;
    [SerializeField] private DialogueDataSO interacaoPosRecompensa;
    private bool popUpAtivado = false;
    private bool armarinhoAberto = false; // Impede que o pop-up apareça após abrir o armarinho

    protected override void DialogueEnded()
    {
        base.DialogueEnded();
        if (armarinhoAberto) return;

        if (!popUpAtivado && popUpObject != null)
        {
            // Modificar o diálogo antes do pop-up baseado na verificação da chave
            int chaveIndex = EncontrarItemNoInventario(chaveArmarinho);
            
            popUpObject.TriggerPopUp("Usar a chave?",
            () => TentarAbrirArmarinho(),
            () => Debug.Log("O jogador recusou abrir o armarinho.")
        );
            
            Debug.Log("Pop-up ativado após o diálogo!");
        }
    }
    public void TentarAbrirArmarinho()
    {
        int chaveIndex = EncontrarItemNoInventario(chaveArmarinho);

        if (chaveIndex != -1)
        {
            playerInventory.RemoveItem(chaveIndex, 5); // Remove uma unidade da chave
            playerInventory.AddItem(recompensa, quantidadeRecompensa);
            Debug.Log("Armarinho aberto! Você recebeu a recompensa.");
            armarinhoAberto = true;
            dialogueData = interacaoPosRecompensa;
            GameEvents.Instance.StartDialogue(posRecompensa);
            //GameEvents.Instance.OnFinishDialogue += DialogueEnded;
        }
        else
        {

            popUpObject.TriggerPopUp("Você não possui a chave",
           () => ClosePopUp(),
           () => Debug.Log("O jogador recusou abrir o armarinho."));
        }
    }

    private int EncontrarItemNoInventario(ItemSO item)
    {
        var inventario = playerInventory.GetCurrentInventoryState();

        foreach (var slot in inventario)
        {
            if (slot.Value.item == item)
            {
                return slot.Key; // Retorna o índice do item no inventário
            }
        }
        return -1; // Retorna -1 caso o item não seja encontrado
    }

    private void ClosePopUp() 
    {
        Debug.Log("Sem chave");
    }
}
