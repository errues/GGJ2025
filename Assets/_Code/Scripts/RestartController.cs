using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartController : MonoBehaviour
{
    private bool mCanSkip;
    private void Start()
    {
        mCanSkip = false;

        StartCoroutine(_EnableSkip());

    }

    private IEnumerator _EnableSkip()
    {
        yield return new WaitForSecondsRealtime(5f);
        mCanSkip = true;
    }
    void Update()
    {
        if(mCanSkip)
        {
            // Detecta cualquier tecla en el teclado o botón en el gamepad.
            if (Input.anyKeyDown)
            {
                LoadScene();
            }
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("SceneMenu");
    }
}
