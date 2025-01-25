using UnityEngine;
using UnityEngine.SceneManagement;


public class GlobalInstaller : MonoBehaviour
{

    void Start()
    {
        BindServices();
    }

    private void BindServices()
    {
    }

    private void Update()
    {
        GlobalManager.Instance.UpdateStateMachine();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
