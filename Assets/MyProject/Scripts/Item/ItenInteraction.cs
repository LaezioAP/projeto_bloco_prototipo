using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItenInteraction : MonoBehaviour
{
    public string itemName = "paçoca";

    public virtual void Interact()
    {
        Debug.Log($"Interagindo com {itemName}");
    }
}
