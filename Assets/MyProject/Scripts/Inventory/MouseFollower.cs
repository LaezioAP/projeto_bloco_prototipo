using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private UIInventoryItem item;

    private void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        Debug.Log($"Canvas encontrado: {canvas != null}");

        item = GetComponentInChildren<UIInventoryItem>();
        Debug.Log($"UIInventoryItem encontrado: {item != null}");
    }

    public void SetData(Sprite sprite, int quantity)  
    {
        item.SetData(sprite, quantity); 
    }

    private void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (
                (RectTransform)canvas.transform,
                Input.mousePosition,
                canvas.worldCamera,
                out position
            );
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool value) 
    {
        Debug.Log($"Item alternado {value}");
        gameObject.SetActive(value);
    }
}
