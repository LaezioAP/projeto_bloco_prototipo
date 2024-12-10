using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private UIInventoryItem itemPrefab; // Prefab para representar cada item
    [SerializeField] private RectTransform contentPanel; // Onde os itens serão listados

    List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem> ();

    public void IniciaUIInventory(int inventorySize) 
    {
        for (int i = 0; i < inventorySize; i++) 
        {
            UIInventoryItem UIitem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            UIitem.transform.SetParent(contentPanel);
            listOfUIItems.Add(UIitem);
        }
    }

    public void Show() 
    {
        gameObject.SetActive (true);
    }
    public void Hide() 
    {
        gameObject.SetActive (false);
    }







   /* private void OnEnable()
    {
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        // Limpa os itens existentes
        foreach (Transform child in contentPanel)
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

            GameObject itemGO = Instantiate(inventoryItemPrefab, contentPanel);

            TMP_Text itemText = itemGO.GetComponentInChildren<TMP_Text>();
            if (itemText == null)
            {
                Debug.LogError("Componente Text não encontrado no prefab do item!");
                continue;
            }

            itemText.text = item.GetItemName(); // Atualiza o texto
        }
    } */
}
