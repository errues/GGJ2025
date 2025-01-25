using UnityEngine;
using UnityEngine.UI;

public class KeyboardButtonHint : MonoBehaviour
{
    [SerializeField]
    private KeyboardButton _buttonTip = KeyboardButton.E_BUTTON;
    [SerializeField]
    private Image _buttonTipImage = null;
    void Start()
    {
        
    }

}

public enum KeyboardButton
{
    LEFT,
    RIGHT,
    UP,
    DOWN,
    ESC_BUTTON,
    ENTER_BUTTON,
    S_BUTTON,
    D_BUTTON,
    W_BUTTON,
    A_BUTTON,
    E_BUTTON,
}
