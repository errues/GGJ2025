using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;

public class ImageToWhiteAndLoadScene : MonoBehaviour
{
    [SerializeField] private Image uiImage; // Referencia al componente Image
    [SerializeField] private float transitionTime = 2.0f;   // Tiempo total de la transición
    [SerializeField] private string sceneToLoad = "NextScene"; // Nombre de la escena a cargar

    [SerializeField] private RawImage rawImage;
    [SerializeField] private VideoPlayer videoPlayer;

    private float timer = 0.0f;

    private void Start()
    {
        if (uiImage == null)
        {
            uiImage = GetComponent<Image>();
        }

        if (uiImage == null)
        {
            Debug.LogError("No se encontró un componente Image en el objeto o asignado manualmente.");
        }

        // Aseg�rate de que el video est� configurado
        if (videoPlayer != null)
        {
            // Suscr�bete al evento loopPointReached
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    private void Update()
    {
        if (uiImage != null && timer < transitionTime)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / transitionTime); // Progreso de 0 a 1

            // Interpolar el color de la imagen hacia blanco
            uiImage.color = Color.Lerp(uiImage.color, Color.white, progress);

            // Si se alcanza el tiempo total, cargar la escena
            if (progress >= 1.0f)
            {
                rawImage.gameObject.SetActive(true);
                videoPlayer?.Play();
            }
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("SceneMenu");
        Debug.Log("El video ha terminado.");
    }

    void OnDestroy()
    {
        // Desuscr�bete del evento para evitar errores
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }

    public void FadeOutRawImage(RawImage rawImage, float duration)
    {
        StartCoroutine(FadeOutCoroutine(rawImage, duration));
    }

    private IEnumerator FadeOutCoroutine(RawImage rawImage, float duration)
    {
        if (rawImage == null)
        {
            Debug.LogError("RawImage es nulo. Aseg�rate de asignar un RawImage.");
            yield break;
        }

        Color originalColor = rawImage.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(originalColor.a, 0f, elapsedTime / duration);
            rawImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Asegura que la opacidad sea completamente 0 al finalizar
        rawImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }
}
