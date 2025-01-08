using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model 
{
    public class ItemEquipavel : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equipado";

        public AudioClip actionSFX { get; private set; }

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            throw new System.NotImplementedException();
        }
    }

}
