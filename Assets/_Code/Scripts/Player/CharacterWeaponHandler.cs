using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CharacterWeaponHandler : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private int _currentWeaponIndex = 0;
    [SerializeField] private int _totalWeaponsAmount = 3;

    public Animator CurrentWeaponAnimator => Current.Animator;
    [SerializeField] private Weapon[] _weapons;

    public Weapon Current => _weapons[_currentWeaponIndex];
    public UnityAction<Weapon> OnWeaponChanged;


    private void Awake()
    {
        _playerInput.actions["Attack"].performed += OnAttack;
        _playerInput.actions["Previous"].performed += OnPrevious;
        _playerInput.actions["Next"].performed += OnNext;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        Current.Attack();
    }

    private async void OnPrevious(InputAction.CallbackContext context)
    {
        Current.Hide();
        await Task.Delay(1000);
        Current.Animator.gameObject.SetActive(false);

        _currentWeaponIndex = (_currentWeaponIndex - 1 + _totalWeaponsAmount) % _totalWeaponsAmount;
        Current.Show();
        OnWeaponChanged(Current);
    }

    private async void OnNext(InputAction.CallbackContext context)
    {
        Current.Hide();
        await Task.Delay(1000);
        Current.Animator.gameObject.SetActive(false);
        
        _currentWeaponIndex = (_currentWeaponIndex + 1 + _totalWeaponsAmount) % _totalWeaponsAmount;
        Current.Show();
        OnWeaponChanged(Current);
    }
}