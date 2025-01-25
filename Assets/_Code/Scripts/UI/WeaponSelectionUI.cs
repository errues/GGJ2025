using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionUI : MonoBehaviour
{
    [SerializeField] private CharacterWeaponHandler _characterWeaponHandler;
    [SerializeField] private Image _selectedWeaponImage;

    private void OnEnable()
    {
        _characterWeaponHandler.OnWeaponChanged += SelectCurrentWeapon;
        SelectCurrentWeapon(_characterWeaponHandler.Current);
    }

    private void OnDisable()
    {
        _characterWeaponHandler.OnWeaponChanged -= SelectCurrentWeapon;
    }


    private void SelectCurrentWeapon(Weapon currentWeapon)
    {
        _selectedWeaponImage.sprite = currentWeapon.Model.PreviewSprite;
    }

}
