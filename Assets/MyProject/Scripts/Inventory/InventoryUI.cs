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
        [SerializeField] private UIInventoryItem itemPrefab; // Prefab do item da UI do inventário
        [SerializeField] private RectTransform contentPanel; // Painel onde os itens serão exibidos
        [SerializeField] private DescriptionUI descriptionUI; // Componente para exibir a descrição dos itens
        [SerializeField] private MouseFollower mouseFollower; // Componente para seguir o mouse com o item arrastado

        List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>(); // Lista para armazenar os itens da UI do inventário

        private int currentDraggedItemIndex = -1; // Índice do item que está sendo arrastado

        // Eventos para comunicação com outros scripts
        public event Action<int> OnDescriptionRequested, // Evento quando a descrição de um item é requisitada
            OnItemActionRequested, // Evento quando uma ação é requisitada para um item
            OnStartDragging; // Evento quando o arrastar de um item começa
        public event Action<int, int> OnSwapItems; // Evento quando dois itens são trocados

        [SerializeField] private ItemActionPanel actionPanel; // Painel de ações do item, serializado

        private void Awake() 
        {
            Hide(); // Esconde a UI do inventário
            mouseFollower.Toggle(false); // Desativa o objeto que segue o mouse
            descriptionUI.ResetDescription(); // Reseta a descrição do item
        }

        private void HandleShowItemActions(UIInventoryItem inventoryItemUI) // Lida com a exibição das ações do item (clique com o botão direito)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI); // Obtém o índice do item na lista
            if (index == -1) // Se o item não foi encontrado
            {
                return; // Sai da função
            }
            OnItemActionRequested?.Invoke(index); // Invoca o evento OnItemActionRequested passando o índice do item
        }

        private void HandleEndDrag(UIInventoryItem inventoryItemUI) // Lida com o fim do arrastar do item
        {
            ResetDraggedItem(); // Reseta o item arrastado
        }

        private void HandleSwap(UIInventoryItem inventoryItemUI) // Lida com a troca de itens
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI); // Obtém o índice do item na lista
            if (index == -1) // Se o item não foi encontrado
            {
                return; // Sai da função
            }
            OnSwapItems?.Invoke(currentDraggedItemIndex, index); // Invoca o evento OnSwapItems passando os índices dos itens a serem trocados
            HandleItemSelection(inventoryItemUI); // Seleciona o item após a troca
        }

        private void ResetDraggedItem() // Reseta o item arrastado
        {
            mouseFollower.Toggle(false); // Desativa o objeto que segue o mouse
            currentDraggedItemIndex = -1; // Reseta o índice do item arrastado
        }

        private void HandleBeginDrag(UIInventoryItem inventoryItemUI) // Lida com o início do arrastar do item
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI); // Obtém o índice do item na lista
            if (index == -1) // Se o item não foi encontrado
                return; // Sai da função
            currentDraggedItemIndex = index; // Define o índice do item arrastado
            HandleItemSelection(inventoryItemUI); // Seleciona o item que está sendo arrastado
            OnStartDragging?.Invoke(index); // Invoca o evento OnStartDragging passando o índice do item
        }

        public void CreateDraggedItem(Sprite sprite, int quantity) // Cria o item que segue o mouse durante o arrastar
        {
            mouseFollower.Toggle(true); // Ativa o objeto que segue o mouse
            mouseFollower.SetData(sprite, quantity); // Define os dados do item que segue o mouse
        }

        private void HandleItemSelection(UIInventoryItem inventoryItemUI) // Lida com a seleção de um item
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI); // Obtém o índice do item na lista
            if (index == -1) // Se o item não foi encontrado
                return; // Sai da função
            OnDescriptionRequested?.Invoke(index); // Invoca o evento OnDescriptionRequested passando o índice do item
        }

        public void Show() // Mostra a UI do inventário
        {
            gameObject.SetActive(true); // Ativa o objeto da UI
            ResetSelection(); // Reseta a seleção dos itens
        }

        public void ResetSelection() // Reseta a seleção dos itens
        {
            descriptionUI.ResetDescription(); // Reseta a descrição do item
            DeselectAllItems(); // Desseleciona todos os itens
        }

        public void AddAction(string actionName, Action performAction) // Adiciona uma ação ao painel de ações
        {
            actionPanel.AddButton(actionName, performAction); // Adiciona um botão ao painel de ações
        }

        public void ShowItemAction(int itemIndex) // Exibe o painel de ações para um item específico
        {
            actionPanel.Toggle(true); // Ativa o painel de ações
            actionPanel.transform.position = listOfUIItems[itemIndex].transform.position; // Define a posição do painel de ações próximo ao item
        }

        private void DeselectAllItems() // Desseleciona todos os itens
        {
            foreach (UIInventoryItem item in listOfUIItems) // Itera sobre todos os itens da lista
            {
                item.Deselect(); // Desseleciona o item
            }
            actionPanel.Toggle(false); // Desativa o painel de ações
        }

        public void Hide() // Esconde a UI do inventário
        {
            actionPanel.Toggle(false); // Desativa o painel de ações
            gameObject.SetActive(false); // Desativa o objeto da UI
            ResetDraggedItem(); // Reseta o item arrastado
        }

        public void IniciaUIInventory(int inventorySize) // Inicializa a UI do inventário
        {
            for (int i = 0; i < inventorySize; i++) // Cria os itens da UI de acordo com o tamanho do inventário
            {
                UIInventoryItem UIitem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity); // Instancia o prefab do item
                UIitem.transform.SetParent(contentPanel); // Define o item como filho do painel de conteúdo
                listOfUIItems.Add(UIitem); // Adiciona o item à lista
                // Adiciona listeners para os eventos do item
                UIitem.OnItemClicked += HandleItemSelection;
                UIitem.OnItemBeginDrag += HandleBeginDrag;
                UIitem.OnItemDroppedOn += HandleSwap;
                UIitem.OnItemEndDrag += HandleEndDrag;
                UIitem.OnRightMouseBtnClick += HandleShowItemActions;
            }
        }

        internal void ResetAllItems() // Reseta todos os itens da UI
        {
            foreach (var item in listOfUIItems) // Itera sobre todos os itens
            {
                item.ResetData(); // Reseta os dados do item
                item.Deselect(); // Desseleciona o item
            }
        }

        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description) // Atualiza a descrição do item
        {
            descriptionUI.SetDescription(itemImage, name, description); // Define a descrição no componente DescriptionUI
            DeselectAllItems(); // Desseleciona todos os itens
            listOfUIItems[itemIndex].Select(); // Seleciona o item cuja descrição foi atualizada
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity) // Atualiza os dados de um item específico na UI do inventário
        {
            if (listOfUIItems.Count > itemIndex) // Verifica se o índice do item é válido (se existe um item nesse índice na lista)
            {
                listOfUIItems[itemIndex].SetData(itemImage, itemQuantity); // Chama a função SetData do item da UI para atualizar sua imagem (Sprite) e quantidade
            }
        }
    }
}