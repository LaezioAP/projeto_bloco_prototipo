using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    [SerializeField]
    private ItemEquipavel weapon;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private List<ItemParameter> parametersYoModify, itemCurrentState;

    public void SetWeapon(ItemEquipavel weaponItemSO, List<ItemParameter> itemState) 
    {
        if (weapon != null) 
        {
            inventoryData.AddItem(weapon, 1, itemCurrentState);
        }

        this.weapon = weaponItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        ModifyParameters();
    }

    private void ModifyParameters() 
    {
        foreach (var parameter in parametersYoModify) 
        {
            if (itemCurrentState.Contains(parameter)) 
            {
                int index = itemCurrentState.IndexOf(parameter);
                float newValue = itemCurrentState[index].value + parameter.value;
                itemCurrentState[index] = new ItemParameter
                {
                    itemParameter = parameter.itemParameter,
                    value = newValue
                };
            }
        }
    }
}
