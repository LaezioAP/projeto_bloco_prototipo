using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    // Cria um ScriptableObject no menu do Unity
    [CreateAssetMenu]
    public class ItemParameterSO : ScriptableObject
    {
        // Nome do parâmetro do item (ex: "Dano", "Defesa", "Durabilidade")
        [field: SerializeField]
        public string ParameterName { get; private set; }
    }
}
