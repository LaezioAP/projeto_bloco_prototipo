using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reportagem : NPCs
{
    [SerializeField] private PopUpObject popUpObject;
    [SerializeField] private InventorySO playerInventory;
    [SerializeField] private ItemSO pilha;
    [SerializeField] private DialogueDataSO posRecompensa;
    [SerializeField] private DialogueDataSO interacaoPosRecompensa;
    private bool popUpAtivado = false;
    private bool tvLigada = false; // Impede que o pop-up apareça após ligar a Tv

    protected override void DialogueEnded()
    {
        base.DialogueEnded();
        if (tvLigada) return;

        if (!popUpAtivado && popUpObject != null)
        {
            // Modificar o diálogo antes do pop-up baseado na verificação da pilha
            int pilhaIndex = EncontrarItemNoInventario(pilha);

            popUpObject.TriggerPopUp("Usar a pilha?",
            () => TentarLigarTv(),
            () => Debug.Log("O jogador recusou abrir o armarinho.")
        );

        }
    }
    public void TentarLigarTv()
    {
        int pilhaIndex = EncontrarItemNoInventario(pilha);

        if (pilhaIndex != -1)
        {
            playerInventory.RemoveItem(pilhaIndex, 1); // Remove uma unidade da pilha
            tvLigada = true;
            dialogueData = interacaoPosRecompensa;
            GameEvents.Instance.StartDialogue(posRecompensa);
            //GameEvents.Instance.OnFinishDialogue += DialogueEnded;
        }
        else
        {

            popUpObject.TriggerPopUp("Você não possui a pilha",
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
        Debug.Log("Sem pilha");
    }
}
