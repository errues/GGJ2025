using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TMPHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI tmpText;
    [SerializeField] GameObject hover;

    public float shakeAmount = 5f;
    public float decreaseFactor = .5f;

    private Vector3 originalPosition;

    void OnEnable()
    {
        originalPosition = transform.localPosition;
    }

    void OnDisable(){
        transform.localPosition = originalPosition;
    }

    void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
    }
    void Update(){
        if(hover.activeSelf){
            hover.transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
     Debug.Log("Hovering Enter");
        hover.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
         Debug.Log("Hovering Exit");
         hover.SetActive(false);
    }
}
