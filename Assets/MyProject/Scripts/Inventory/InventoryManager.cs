using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventoryUI inventoryUI;

    public int inventorySize = 10;


    private void Start()
    {
        inventoryUI.IniciaUIInventory(inventorySize);
    }

    private void Update()
    {
        // Liga e desliga o inventário ao pressionar "I"
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
            }
            else 
            {
                inventoryUI.Hide();
            }
        }
    }

}
