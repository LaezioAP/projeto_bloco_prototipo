using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoToMainMenu : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Referência ao Video Player

    void Start()
    {
        // Verifica se o VideoPlayer está atribuído
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // Associa o evento de término do vídeo
        videoPlayer.loopPointReached += OnVideoEnd;

        // Toca o vídeo automaticamente (opcional)
        videoPlayer.Play();
    }

    // Função chamada quando o vídeo termina
    void OnVideoEnd(VideoPlayer vp)
    {
        // Volta para a cena 0 (cena inicial)
        SceneManager.LoadScene(0);
    }
}