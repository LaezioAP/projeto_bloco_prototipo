using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

namespace Inventory.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private UIInventoryItem itemPrefab; // Prefab para representar cada item
        [SerializeField] private RectTransform contentPanel; // Onde os itens serão listados
        [SerializeField] private DescriptionUI descriptionUI;
        [SerializeField] private MouseFollower mouseFollower;

        List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

        private int currentDraggedItemIndex = -1;

        public event Action<int> OnDescriptionRequested,
            OnItemActionRequested, OnStartDragging;
        public event Action<int, int> OnSwapItems;

        [SerializeField]
        private ItemActionPanel actionPanel;

        private void Awake()
        {
            Hide();
            mouseFollower.Toggle(false);
            descriptionUI.ResetDescription();
        }

        private void HandleShowItemActions(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnItemActionRequested?.Invoke(index);

        }
        private void HandleEndDrag(UIInventoryItem inventoryItemUI)
        {
            ResetDraggedItem();
        }
        private void HandleSwap(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnSwapItems?.Invoke(currentDraggedItemIndex, index);
            HandleItemSelection(inventoryItemUI);
        }

        private void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currentDraggedItemIndex = -1;
        }

        private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            currentDraggedItemIndex = index;
            HandleItemSelection(inventoryItemUI);
            OnStartDragging?.Invoke(index);
        }

        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, quantity);
        }

        private void HandleItemSelection(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            OnDescriptionRequested?.Invoke(index);
        }
        public void Show()
        {
            gameObject.SetActive(true);
            ResetSelection();
        }

        public void ResetSelection()
        {
            descriptionUI.ResetDescription();
            DeselectAllItems();
        }

        public void AddAction(string actionName, Action performAction) 
        {
            actionPanel.AddButton(actionName, performAction);
        }

        public void ShowItemAction(int itemIndex) 
        {
            actionPanel.Toggle(true);
            actionPanel.transform.position = listOfUIItems[itemIndex].transform.position;
        }

        private void DeselectAllItems()
        {
            foreach (UIInventoryItem item in listOfUIItems)
            {
                item.Deselect();
            }
            actionPanel.Toggle(false);  
        }

        public void Hide()
        {
            actionPanel.Toggle(false);
            gameObject.SetActive(false);
            ResetDraggedItem();
        }

        public void IniciaUIInventory(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                UIInventoryItem UIitem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                UIitem.transform.SetParent(contentPanel);
                listOfUIItems.Add(UIitem);
                UIitem.OnItemClicked += HandleItemSelection;
                UIitem.OnItemBeginDrag += HandleBeginDrag;
                UIitem.OnItemDroppedOn += HandleSwap;
                UIitem.OnItemEndDrag += HandleEndDrag;
                UIitem.OnRightMouseBtnClick += HandleShowItemActions;
            }
        }

        internal void ResetAllItems()
        {
            foreach (var item in listOfUIItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }

        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
        {
            descriptionUI.SetDescription(itemImage, name, description);
            DeselectAllItems();
            listOfUIItems[itemIndex].Select();
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (listOfUIItems.Count > itemIndex)
            {
                listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
            }
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
}