using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingPoint : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contactCount > 0)
        {
            // Pegamos o primeiro ponto de contato da colisão
            Vector2 pontoColisao = collision.GetContact(0).point;

            // Atualiza a ordem de renderização com base na posição Y do ponto de colisão
            AtualizarSortingOrder(pontoColisao);
        }
    }

    private void AtualizarSortingOrder(Vector2 ponto)
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(-ponto.y * 100);
    }
}
