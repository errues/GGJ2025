using MyBox;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CharacterWeaponHandler : MonoBehaviour {
    private PlayerInput _playerInput;
    private int _currentWeaponIndex = 0;
    private int _totalWeaponsAmount => _weapons.Length;

    [SerializeField] private Weapon[] _weapons;
    [SerializeField] private Weapon _specialWeapon;

    public Weapon Current {
        get {
            if (_usingSpecialWeapon)
                return _specialWeapon;
            else
                return _weapons[_currentWeaponIndex];
        }
    }

    public int CurrentIndex {
        get {
            if (_usingSpecialWeapon)
                return _weapons.Length;
            else
                return _currentWeaponIndex;
        }
    }

    public Animator CurrentWeaponAnimator => Current.Animator;

    public UnityAction<int> OnWeaponChanged { get; set; }

    private bool _usingSpecialWeapon = false;
    private bool _isChangingWeapon;

    private Coroutine _changeWeaponCoroutine;


    private void Awake() {
        _playerInput = GetComponentInParent<PlayerInput>();
        _playerInput.actions["Attack"].performed += OnAttack;
        _playerInput.actions["Previous"].performed += OnPrevious;
        _playerInput.actions["Next"].performed += OnNext;
    }

    private void OnAttack(InputAction.CallbackContext context) {
        if (_isChangingWeapon) return;

        Current.Attack();
    }

    private void OnPrevious(InputAction.CallbackContext context) {
        if (_usingSpecialWeapon) return;

        _changeWeaponCoroutine = StartCoroutine(ChangeWeaponIndex(_currentWeaponIndex - 1));
    }

    private void OnNext(InputAction.CallbackContext context) {
        if (_usingSpecialWeapon) return;

        _changeWeaponCoroutine = StartCoroutine(ChangeWeaponIndex(_currentWeaponIndex + 1));
    }

    private IEnumerator ChangeWeaponIndex(int index) {
        if (_isChangingWeapon) yield break;

        _isChangingWeapon = true;
        Current.Hide();
        yield return new WaitForSeconds(1);
        Current.Animator.gameObject.SetActive(false);

        _usingSpecialWeapon = false;
        _currentWeaponIndex = (index + _totalWeaponsAmount) % _totalWeaponsAmount;

        Current.Show();
        OnWeaponChanged(CurrentIndex);
        _isChangingWeapon = false;
    }

    [ButtonMethod]
    public void SetSpecialWeapon() {
        if (_changeWeaponCoroutine != null)
            StopCoroutine(_changeWeaponCoroutine);

        StartCoroutine(ChangeToSpecialWeapon());
    }

    public void FinishSpecialWeapon() {
        _changeWeaponCoroutine = StartCoroutine(ChangeWeaponIndex(_currentWeaponIndex));
    }

    private IEnumerator ChangeToSpecialWeapon() {
        _isChangingWeapon = true;
        Current.Hide();
        yield return new WaitForSeconds(1);
        Current.Animator.gameObject.SetActive(false);

        _usingSpecialWeapon = true;

        Current.Show();
        OnWeaponChanged(CurrentIndex);
        _isChangingWeapon = false;

        yield return new WaitForSeconds(5);

        ((Karcher)Current).ActivateKarcher();
    }
}