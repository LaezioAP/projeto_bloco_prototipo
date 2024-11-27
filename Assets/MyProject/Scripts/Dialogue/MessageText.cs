using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class MessageText : MonoBehaviour
{
    [SerializeField] private float intervaloEntreLetras = 0.5f;
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    public IEnumerator ShowText(string content) 
    {
        text.maxVisibleCharacters = 0;
        text.SetText(content);  
        yield return RevealChars();
    }

    public void HideText() 
    {
        text.SetText("");
        text.maxVisibleCharacters = 0;  
    }

    public void SkipAnimation() 
    {
        text.maxVisibleCharacters = text.textInfo.characterCount;
    }

    private IEnumerator RevealChars() 
    {
        while (text.maxVisibleCharacters <= text.textInfo.characterCount) 
        {
            yield return new WaitForSeconds(intervaloEntreLetras);
            text.maxVisibleCharacters++;
        }
    }
}
