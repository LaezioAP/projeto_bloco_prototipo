using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryItemPrefab; // Prefab para representar cada item
    [SerializeField] private Transform inventoryPanel; // Onde os itens serão listados

    private void OnEnable()
    {
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        // Limpa os itens existentes
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        var items = InventoryManager.Instance?.GetInventoryItems();
        if (items == null || items.Count == 0)
        {
            Debug.Log("Nenhum item no inventário.");
            return;
        }

        // Adiciona os itens atuais do inventário
        foreach (var item in items)
        {
            if (item == null)
            {
                Debug.LogWarning("Item nulo encontrado no inventário.");
                continue;
            }

            GameObject itemGO = Instantiate(inventoryItemPrefab, inventoryPanel);
            
            Text itemText = itemGO.GetComponentInChildren<Text>();
            if (itemText == null)
            {
                Debug.LogError("Componente Text não encontrado no prefab do item!");
                continue;
            }

            itemText.text = item.itemName; // Atualiza o texto
        }
    }
}
