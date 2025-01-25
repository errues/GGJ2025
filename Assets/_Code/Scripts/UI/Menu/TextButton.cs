using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TextButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshPro textComponent;

    [SerializeField] public Scene sceneToOpen;

    private void Awake()
    {
        if (textComponent == null)
            textComponent = GetComponent<TextMeshPro>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      
        // Add your click handling logic here
        switch (gameObject.name)
        {
            case "Play":
                SceneManager.LoadScene(sceneToOpen.name);
            
            break;

            case "Options":

            break;

            case "Exit":
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
            
            break;

            default:
              Debug.Log("TextMeshPro text clicked!");
            break;
        }

    }
}