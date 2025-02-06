using Inventory.UI; 
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

public class MouseFollower : MonoBehaviour 
{
    [SerializeField] private Canvas canvas; // Variável para armazenar o canvas onde o item será exibido
    [SerializeField] private UIInventoryItem item; // Variável para armazenar o item da UI do inventário

    private void Awake() 
    {
        canvas = transform.root.GetComponent<Canvas>(); // Obtém o componente Canvas do objeto raiz (pai) deste objeto
        Debug.Log($"Canvas encontrado: {canvas != null}"); 

        item = GetComponentInChildren<UIInventoryItem>(); // Obtém o componente UIInventoryItem dos filhos deste objeto
        Debug.Log($"UIInventoryItem encontrado: {item != null}"); 
    }

    public void SetData(Sprite sprite, int quantity) // Função para definir os dados do item (sprite e quantidade)
    {
        item.SetData(sprite, quantity); // Chama a função SetData do componente UIInventoryItem para atualizar a imagem e a quantidade do item na UI
    }

    private void Update() 
    {
        Vector2 position; // Variável para armazenar a posição do mouse no canvas

        // Converte a posição do mouse na tela para a posição local no canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (
                (RectTransform)canvas.transform, // Transforma do canvas
                Input.mousePosition, // Posição do mouse na tela
                canvas.worldCamera, // Câmera do mundo do canvas
                out position // Variável de saída para a posição local no canvas
            );

        transform.position = canvas.transform.TransformPoint(position); // Define a posição deste objeto (MouseFollower) para a posição calculada no canvas
    }

    public void Toggle(bool value) // Função para ativar/desativar o objeto MouseFollower
    {
        Debug.Log($"Item alternado {value}"); 
        gameObject.SetActive(value); // Ativa ou desativa o objeto MouseFollower
    }
}