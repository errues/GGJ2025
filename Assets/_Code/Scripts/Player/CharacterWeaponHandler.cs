using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CharacterWeaponHandler : MonoBehaviour
{
    private PlayerInput _playerInput;
    private int _currentWeaponIndex = 0;
    private int _totalWeaponsAmount => _weapons.Length;

    [SerializeField] private Weapon[] _weapons;
    [SerializeField] private Weapon _specialWeapon;

    public Weapon Current
    {
        get
        {
            if (_usingSpecialWeapon)
                return _specialWeapon;
            else
                return _weapons[_currentWeaponIndex];
        }
    }

    public Animator CurrentWeaponAnimator => Current.Animator;
    
    public UnityAction<Weapon> OnWeaponChanged;

    private bool _usingSpecialWeapon = false;
    private bool _isChangingWeapon;

    private Coroutine _changeWeaponCoroutine;


    private void Awake()
    {
        _playerInput = GetComponentInParent<PlayerInput>();
        _playerInput.actions["Attack"].performed += OnAttack;
        _playerInput.actions["Previous"].performed += OnPrevious;
        _playerInput.actions["Next"].performed += OnNext;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (_isChangingWeapon) return;

        Current.Attack();
    }

    private void OnPrevious(InputAction.CallbackContext context)
    {
        if (_usingSpecialWeapon) return;

        _changeWeaponCoroutine = StartCoroutine(ChangeWeaponIndex(_currentWeaponIndex - 1));
    }

    private void OnNext(InputAction.CallbackContext context)
    {
        if (_usingSpecialWeapon) return;

        _changeWeaponCoroutine = StartCoroutine(ChangeWeaponIndex(_currentWeaponIndex + 1));
    }

    private IEnumerator ChangeWeaponIndex(int index)
    {
        if (_isChangingWeapon) yield break;

        _isChangingWeapon = true;
        Current.Hide();
        yield return new WaitForSeconds(1);
        Current.Animator.gameObject.SetActive(false);

        _currentWeaponIndex = (index + _totalWeaponsAmount) % _totalWeaponsAmount;

        Current.Show();
        OnWeaponChanged(Current);
        _isChangingWeapon = false;
    }

    private void SetSpecialWeapon()
    {
        if (_changeWeaponCoroutine != null)
            StopCoroutine(_changeWeaponCoroutine);

        StartCoroutine(ChangeToSpecialWeapon());
    }

    private void FinishSpecialWeapon()
    {
        _usingSpecialWeapon = false;
        _changeWeaponCoroutine = StartCoroutine(ChangeWeaponIndex(_currentWeaponIndex));
    }

    private IEnumerator ChangeToSpecialWeapon()
    {
        _isChangingWeapon = true;
        Current.Hide();
        yield return new WaitForSeconds(1);
        Current.Animator.gameObject.SetActive(false);

        _usingSpecialWeapon = true;

        Current.Show();
        OnWeaponChanged(Current);
        _isChangingWeapon = false;
    }
}