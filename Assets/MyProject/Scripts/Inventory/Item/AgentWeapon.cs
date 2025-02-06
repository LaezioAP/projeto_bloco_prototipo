using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    // Arma atualmente equipada pelo personagem
    [SerializeField]
    private ItemEquipavel weapon;

    // Referência ao inventário do personagem
    [SerializeField]
    private InventorySO inventoryData;

    // Lista de parâmetros que modificam a arma e o estado atual da arma equipada
    [SerializeField]
    private List<ItemParameter> parametersYoModify, itemCurrentState;

    // Troca a arma equipada pelo personagem
    public void SetWeapon(ItemEquipavel weaponItemSO, List<ItemParameter> itemState)
    {
        // Se já houver uma arma equipada, adiciona ao inventário antes de trocar
        if (weapon != null)
        {
            inventoryData.AddItem(weapon, 1, itemCurrentState);
        }

        // Atualiza a nova arma equipada e seu estado
        this.weapon = weaponItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);

        // Aplica modificadores nos atributos da nova arma
        ModifyParameters();
    }

    // Modifica os atributos da arma equipada com base nos modificadores
    private void ModifyParameters()
    {
        foreach (var parameter in parametersYoModify)
        {
            // Se o parâmetro já existe na arma atual, modifica seu valor
            if (itemCurrentState.Contains(parameter))
            {
                int index = itemCurrentState.IndexOf(parameter);
                float newValue = itemCurrentState[index].value + parameter.value;

                // Cria um novo objeto ItemParameter para atualizar a lista sem erro
                itemCurrentState[index] = new ItemParameter
                {
                    itemParameter = parameter.itemParameter,
                    value = newValue
                };
            }
        }
    }
}
