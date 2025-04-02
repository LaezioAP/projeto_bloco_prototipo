using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage; // Referência ao painel de fade
    public float fadeDuration = 1f; // Duração do fade em segundos

    private void Start()
    {
        // Garante que o painel comece opaco e faça fade-in ao carregar a cena
        fadeImage.color = Color.black;
        StartCoroutine(FadeIn());
    }

    // Função para iniciar o fade e trocar de cena
    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    // Fade-in (simula abrir os olhos)
    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // De opaco para transparente
            fadeImage.color = color;
            yield return null;
        }
        color.a = 0f; // Garante que fica totalmente transparente
        fadeImage.color = color;
    }

    // Fade-out (escurece antes de trocar de cena)
    IEnumerator FadeOut(string sceneName)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration); // De transparente para opaco
            fadeImage.color = color;
            yield return null;
        }
        color.a = 1f; // Garante que fica totalmente opaco
        fadeImage.color = color;

        // Troca para a nova cena
        SceneManager.LoadScene(sceneName);
    }
}