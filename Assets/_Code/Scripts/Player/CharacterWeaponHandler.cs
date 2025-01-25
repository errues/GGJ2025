using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CharacterWeaponHandler : MonoBehaviour
{
    private PlayerInput _playerInput;
    private int _currentWeaponIndex = 0;
    private int _totalWeaponsAmount = 3;

    public Animator CurrentWeaponAnimator => Current.Animator;
    [SerializeField] private Weapon[] _weapons;

    public Weapon Current => _weapons[_currentWeaponIndex];
    public UnityAction<Weapon> OnWeaponChanged;

    private bool _isChangingWeapon;


    private void Awake()
    {
        _playerInput = GetComponentInParent<PlayerInput>();
        _playerInput.actions["Attack"].performed += OnAttack;
        _playerInput.actions["Previous"].performed += OnPrevious;
        _playerInput.actions["Next"].performed += OnNext;

        _totalWeaponsAmount = _weapons.Length;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        Current.Attack();
    }

    private async void OnPrevious(InputAction.CallbackContext context)
    {
        await ChangeWeaponIndex(-1);
    }

    private async void OnNext(InputAction.CallbackContext context)
    {
        await ChangeWeaponIndex(1);
    }

    private async Task ChangeWeaponIndex(int direction)
    {
        if (_isChangingWeapon) return;

        _isChangingWeapon = true;
        Current.Hide();
        await Task.Delay(1000);
        Current.Animator.gameObject.SetActive(false);

        _currentWeaponIndex = (_currentWeaponIndex + direction + _totalWeaponsAmount) % _totalWeaponsAmount;

        Current.Show();
        OnWeaponChanged(Current);
        _isChangingWeapon = false;
    }

}