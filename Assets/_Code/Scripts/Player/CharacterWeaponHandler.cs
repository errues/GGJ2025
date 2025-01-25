using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CharacterWeaponHandler : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private int _currentWeaponIndex = 0;
    [SerializeField] private int _totalWeaponsAmount = 3;

    private Animator _currentWeaponAnimator;
    [SerializeField] private Weapon[] _weapons;

    public Weapon Current => _weapons[_currentWeaponIndex];
    public UnityAction<Weapon> OnWeaponChanged;


    private void Awake()
    {
        _currentWeaponAnimator = Current.Animator;
    }

    private void OnAttack(InputValue _)
    {
        Current.Attack();
    }

    private void OnPrevious(InputValue _)
    {
        Current.Hide();

        _weapons[_currentWeaponIndex].gameObject.SetActive(false);
        _currentWeaponIndex = (_currentWeaponIndex - 1 + _totalWeaponsAmount) % _totalWeaponsAmount;

        Current.Show();


        _weapons[_currentWeaponIndex].gameObject.SetActive(true);

        OnWeaponChanged(Current);
    }

    private void OnNext(InputValue _)
    {
        _weapons[_currentWeaponIndex].gameObject.SetActive(false);
        _currentWeaponIndex = (_currentWeaponIndex + 1 + _totalWeaponsAmount) % _totalWeaponsAmount;
        _weapons[_currentWeaponIndex].gameObject.SetActive(true);

        OnWeaponChanged(Current);
    }
}