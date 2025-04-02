using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneFader sceneFader; // Referência ao SceneFader

    public void PlayGame()
    {
        sceneFader.FadeToScene("Jogo"); 
    }

}