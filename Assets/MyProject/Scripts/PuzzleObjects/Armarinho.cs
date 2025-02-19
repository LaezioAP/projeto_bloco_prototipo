using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armarinho : NPCs
{
    [SerializeField] private PopUpObject popUpObject;
    private bool popUpAtivado = false;

    protected override void DialogueEnded()
    {
        base.DialogueEnded();

        if (!popUpAtivado && popUpObject != null)
        {
            popUpAtivado = true; // Garante que não dispara duas vezes
            popUpObject.TriggerPopUp();
            Debug.Log("Pop-up ativado após o diálogo!");
        }
    }
}
