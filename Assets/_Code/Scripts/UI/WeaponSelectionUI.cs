using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionUI : MonoBehaviour
{
    private CharacterWeaponHandler _characterWeaponHandler;
    [SerializeField] private Animator _selectedWeaponAnimator;
    //[SerializeField] private Image _selectedWeaponImage;

    private void OnEnable()
    {
        if (_characterWeaponHandler == null)
        {
            _characterWeaponHandler = FindFirstObjectByType<CharacterWeaponHandler>();
        }

        _characterWeaponHandler.OnWeaponChanged += SelectCurrentWeapon;
        SelectCurrentWeapon(_characterWeaponHandler.CurrentIndex);
    }

    private void OnDisable()
    {
        _characterWeaponHandler.OnWeaponChanged -= SelectCurrentWeapon;
    }


    private void SelectCurrentWeapon(int currentWeapon)
    {
        //_selectedWeaponImage.sprite = currentWeapon.Model.PreviewSprite;
        _selectedWeaponAnimator.SetInteger("weapon_selected", currentWeapon);
    }

}
