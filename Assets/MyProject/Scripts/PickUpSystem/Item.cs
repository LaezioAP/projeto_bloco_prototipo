using Inventory.Model; 
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

public class Item : MonoBehaviour
{
    [field: SerializeField]
    public ItemSO InventoryItem { get; private set; } // Referência ao Scriptable Object que contém os dados do item (imagem, nome, etc.).

    [field: SerializeField]
    public int Quantity { get; set; } = 1; // Quantidade do item (ex: 1 moeda, 5 flechas, etc.).

    [SerializeField]
    private AudioSource audioSource; // Componente de áudio para tocar um som ao coletar o item.

    [SerializeField]
    private float duration = 0.3f; // Duração da animação de coleta do item.

    private void Start()
    {
        // Define a imagem do item no SpriteRenderer usando a imagem do Scriptable Object.
        GetComponent<SpriteRenderer>().sprite = InventoryItem.ItemImage;
    }

    public void DestroyItem()
    {
        // Desativa o colisor do item para evitar que ele seja coletado novamente.
        GetComponent<Collider2D>().enabled = false;

        // Inicia a animação de coleta do item usando uma corrotina.
        StartCoroutine(AnimateItemPickup());
    }

    private IEnumerator AnimateItemPickup()
    {
        // Toca o som de coleta do item.
        audioSource.Play();

        // Guarda o tamanho inicial do item.
        Vector3 startScale = transform.localScale;

        // Define o tamanho final do item (zero, para que ele desapareça).
        Vector3 endScale = Vector3.zero;

        // Variável para controlar o tempo da animação.
        float currentTime = 0;

        // Loop que executa a animação de redução de tamanho.
        while (currentTime < duration)
        {
            // Incrementa o tempo com base no tempo passado desde o último frame.
            currentTime += Time.deltaTime;

            // Interpola suavemente o tamanho do item entre o tamanho inicial e o final.
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / duration);

            // Pausa a execução até o próximo frame.
            yield return null;
        }

        // Destroi o objeto do item após a animação terminar.
        Destroy(gameObject);
    }
}