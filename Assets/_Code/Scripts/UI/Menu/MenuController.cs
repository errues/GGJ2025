using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{

    private PlayerInput playerInput;
    [SerializeField] private GameObject[] menuList;

    private int selected = -1;
    private Vector2 moveInput;

     private void Awake() {
        playerInput = GetComponent<PlayerInput>();
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

  
}
