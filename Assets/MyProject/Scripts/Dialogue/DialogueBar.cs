using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DialogueBar : MonoBehaviour
{
    private Image barImage;
    private RectTransform rectTransform;

    private Vector2 hiddenPosition = new Vector2(0, -100);
    private Vector2 visiblePosition = Vector2.zero;
    private float animationSpeed = 200;

    private void Awake()
    {
        barImage = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        rectTransform.anchoredPosition = hiddenPosition;
    }

    public IEnumerator ShowBar() 
    {
        while (rectTransform.anchoredPosition.y < visiblePosition.y) 
        {
            rectTransform.anchoredPosition += Vector2.up * animationSpeed * Time.deltaTime;
            yield return null;
        }
        rectTransform.anchoredPosition = visiblePosition;
    }
    
    public IEnumerator HiddeBar() 
    {
        while (rectTransform.anchoredPosition.y > hiddenPosition.y) 
        {
            rectTransform.anchoredPosition -= Vector2.up * animationSpeed * Time.deltaTime;
            yield return null;
        }
        rectTransform.anchoredPosition = hiddenPosition;
    }
}
