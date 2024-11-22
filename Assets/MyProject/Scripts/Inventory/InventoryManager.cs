using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private List<ItemColectable> inventoryItems = new List<ItemColectable>();
    private bool isInventoryOpen = false;

    [SerializeField] private GameObject inventoryUI; // O painel de UI para o inventário

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Garante que o inventário começa fechado
        if (inventoryUI != null)
        {
            inventoryUI.SetActive(false);
        }
    }

    private void Update()
    {
        // Alterna o inventário ao pressionar "I"
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void AddItemToInventory(ItemColectable item)
    {
        inventoryItems.Add(item);
        Debug.Log($"{item.itemName} foi adicionado ao inventário!");
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;

        if (inventoryUI != null)
        {
            inventoryUI.SetActive(isInventoryOpen);
        }
    }

    public List<ItemColectable> GetInventoryItems()
    {
        foreach (var item in InventoryManager.Instance.GetInventoryItems())
        {
            if (item == null)
            {
                Debug.LogError("Item encontrado como null!");
                continue;
            }

            Debug.Log($"Item no inventário: {item.itemName}");
            // Processar o item
        }
        return inventoryItems;
    }
}
