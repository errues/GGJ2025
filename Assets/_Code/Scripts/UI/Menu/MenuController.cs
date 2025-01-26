using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour
{

    private PlayerInput playerInput;
    [SerializeField] private GameObject[] menuList;

    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private RawImage rawImage;

    private int selected = -1;
    private Vector2 moveInput;

     private void Awake() {
        playerInput = GetComponent<PlayerInput>();

        // Aseg�rate de que el video est� configurado
        if (videoPlayer != null)
        {
            // Suscr�bete al evento loopPointReached
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    private void OnNavigate(InputValue value) {
        moveInput = value.Get<Vector2>();
        if(moveInput.y==0){
            return;
        }

        if(moveInput.y>0){
            selected = 0;
             menuList[0].GetComponent<TMPHoverHandler>().OnPointerEnter(null); 
             menuList[1].GetComponent<TMPHoverHandler>().OnPointerExit(null);
        }else{
            selected = 1;
             menuList[0].GetComponent<TMPHoverHandler>().OnPointerExit(null); 
             menuList[1].GetComponent<TMPHoverHandler>().OnPointerEnter(null);
        }
    }

    private void OnSubmit(){
        if(selected!=-1){
            menuList[selected].GetComponent<TextButton>().OnPointerClick(null);
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        FadeOutRawImage(rawImage, 0.1f);
       
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
        // Activa los objetos especificados
        foreach (GameObject obj in menuList)
        {
            obj.SetActive(true);
        }

    }


}
