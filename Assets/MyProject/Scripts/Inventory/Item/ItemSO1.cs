using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    // Classe abstrata para todos os itens do jogo
    public abstract class ItemSO : ScriptableObject
    {
        // Define se o item pode ser empilhado no inventário
        [field: SerializeField]
        public bool IsStackable { get; set; }

        // ID único do item (gerado automaticamente)
        public int ID => GetInstanceID();

        // Define o tamanho máximo da pilha (1 = não empilhável)
        [field: SerializeField]
        public int MaxStackSize { get; set; } = 1;

        // Nome do item
        [field: SerializeField]
        public string Name { get; set; }

        // Descrição do item (com suporte a múltiplas linhas)
        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; }

        // Imagem associada ao item
        [field: SerializeField]
        public Sprite ItemImage { get; set; }

        // Lista de parâmetros padrão do item (exemplo: ataque, defesa, velocidade)
        [field: SerializeField]
        public List<ItemParameter> DefaultParametersList { get; set; }
    }

    // Define parâmetros que podem modificar atributos do personagem
    [Serializable]
    public struct ItemParameter : IEquatable<ItemParameter>
    {
        // Tipo do parâmetro (exemplo: ataque, defesa, vida)
        public ItemParameterSO itemParameter;

        // Valor do parâmetro (exemplo: +10 de ataque, +5 de velocidade)
        public float value;

        // Método para comparar dois parâmetros e verificar se são do mesmo tipo
        public bool Equals(ItemParameter other)
        {
            return other.itemParameter == itemParameter;
        }
    }
}
