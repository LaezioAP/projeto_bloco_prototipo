using Inventory.Model; 
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

public class PickupSystem : MonoBehaviour
{
    [SerializeField] private InventorySO inventoryData; // Variável para armazenar os dados do inventário (Scriptable Object), serializada para ser visível no Inspector da Unity

    private void OnTriggerEnter2D(Collider2D collision) // Função chamada quando um objeto 2D entra na área de colisão do objeto com este script
    {
        Item item = collision.GetComponent<Item>(); // Tenta obter o componente Item do objeto que colidiu

        if (item != null) // Verifica se o objeto que colidiu possui um componente Item
        {
            int remainder = inventoryData.AddItem(item.InventoryItem, item.Quantity); // Chama a função AddItem do inventário para adicionar o item e sua quantidade. remainder armazena a quantidade de itens que não couberam no inventário

            if (remainder == 0) // Verifica se todos os itens foram adicionados ao inventário
            {
                item.DestroyItem(); // Destrói o item do jogo
            }
            else // Se nem todos os itens couberam no inventário
            {
                item.Quantity = remainder; // Atualiza a quantidade de itens no objeto com a quantidade restante
            }
        }
    }
}