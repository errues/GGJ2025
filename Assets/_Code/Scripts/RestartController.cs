using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartController : MonoBehaviour
{
    void Update()
    {
        // Detecta cualquier tecla en el teclado o bot�n en el gamepad.
        if (Input.anyKeyDown)
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("SceneMenu");
    }
}
