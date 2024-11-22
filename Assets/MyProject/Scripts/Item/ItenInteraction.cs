using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItenInteraction : MonoBehaviour
{
    public string itemName = "livro";

    public virtual void Interact()
    {
        Debug.Log($"Interagindo com {itemName}");
    }
}
