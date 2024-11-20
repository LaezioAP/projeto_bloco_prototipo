using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColectable : ItenInteraction
{
    public override void Interact()
    {
        base.Interact();
        Debug.Log($"{itemName} foi coletado!");
        Destroy(gameObject);
    }
}
