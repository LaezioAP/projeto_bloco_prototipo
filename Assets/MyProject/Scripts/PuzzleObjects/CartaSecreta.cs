using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaSecreta : MonoBehaviour
{

    [SerializeField] private InventorySO playerInventory;        // Inventário do jogador
    [SerializeField] private ItemSO[] requiredItems;             // Array com os 5 itens necessários
    [SerializeField] private DialogueDataSO posResolution;       // Diálogo após coletar todos os itens
    [SerializeField] public DialogueDataSO dialogueData;         // Diálogo atual (se necessário)
    [SerializeField] private ItemSO thisItem;                    // O item específico deste objeto

    private bool isCollected = false;                            // Flag para evitar múltiplas coletas

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

    private void VerificarItensEProsseguir()
    {
        bool allItemsCollected = true;
        List<int> itemsToRemove = new List<int>();

        foreach (ItemSO item in requiredItems)
        {
            int itemIndex = EncontrarItemNoInventario(item);
            if (itemIndex == -1)
            {
                allItemsCollected = false;
                Debug.Log($"Faltando item: {item.name}");
                break;
            }
            else
            {
                itemsToRemove.Add(itemIndex);
            }
        }

        if (allItemsCollected)
        {
            foreach (int index in itemsToRemove)
            {
                playerInventory.RemoveItem(index, 1);
            }
            dialogueData = posResolution;
            GameEvents.Instance.StartDialogue(posResolution);
            Debug.Log("Todos os 5 itens coletados! Vá para o porão.");
        }
        else
        {
            Debug.Log("Nem todos os itens foram coletados ainda.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {
            if (thisItem != null)
            {
                int existingIndex = EncontrarItemNoInventario(thisItem);
                if (existingIndex == -1) // Só adiciona se não estiver presente
                {
                    playerInventory.AddItem(thisItem, 1);
                    Debug.Log($"Item {thisItem.name} coletado!");
                }
                else
                {
                    Debug.Log($"Item {thisItem.name} já está no inventário!");
                }

                isCollected = true; // Marca como coletado
                VerificarItensEProsseguir(); // Verifica o inventário

            }
            else
            {
                Debug.LogError("thisItem não está configurado no Inspector!");
            }
        }
    }

}
