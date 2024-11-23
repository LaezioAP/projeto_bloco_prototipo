using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private List<IInventoryItem> inventoryItems = new List<IInventoryItem>();
    private bool isInventoryOpen = false;

    [SerializeField] private GameObject inventoryUI; // O painel de UI para o inventário

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Garante que só exista uma instância do Singleton
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
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

    public void AddItemToInventory(IInventoryItem item)
    {
        if (item == null)
        {
            Debug.LogError("Tentativa de adicionar um item nulo ao inventário!");
            return;
        }

        inventoryItems.Add(item);
        Debug.Log($"{item.GetItemName()} foi adicionado ao inventário!");
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;

        if (inventoryUI != null)
        {
            inventoryUI.SetActive(isInventoryOpen);
        }
    }

    public List<IInventoryItem> GetInventoryItems()
    {
        return inventoryItems;
    }
}
