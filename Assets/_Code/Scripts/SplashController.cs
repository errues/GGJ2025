using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageToWhiteAndLoadScene : MonoBehaviour
{
    [SerializeField] private Image uiImage; // Referencia al componente Image
    [SerializeField] private float transitionTime = 2.0f;   // Tiempo total de la transición
    [SerializeField] private string sceneToLoad = "NextScene"; // Nombre de la escena a cargar

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
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
