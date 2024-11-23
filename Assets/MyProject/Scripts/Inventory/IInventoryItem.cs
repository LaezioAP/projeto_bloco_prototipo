using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem 
{
    string GetItemName();
    void Use(); // Método genérico para usar o item
}
