using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Animator))]
public class CharacterAttack : MonoBehaviour
{
    //private PlayerInput _playerInput;
    [SerializeField] private int _currentWeaponIndex;
    [SerializeField] private int _totalWeaponsAmount = 3;

    private Animator _animator;
    [SerializeField] private GameObject[] _weaponGOs;


    private void Awake()
    {
        //_playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
    }

    private void OnAttack(InputValue _)
    {
        _animator.SetTrigger($"Attack_{_currentWeaponIndex}");
    }

    private void OnPrevious(InputValue _)
    {
        _weaponGOs[_currentWeaponIndex].SetActive(false);
        _currentWeaponIndex = (_currentWeaponIndex - 1 + _totalWeaponsAmount) % _totalWeaponsAmount;
        _weaponGOs[_currentWeaponIndex].SetActive(true);
    }

    private void OnNext(InputValue _)
    {
        _weaponGOs[_currentWeaponIndex].SetActive(false);
        _currentWeaponIndex = (_currentWeaponIndex + 1 + _totalWeaponsAmount) % _totalWeaponsAmount;
        _weaponGOs[_currentWeaponIndex].SetActive(true);
    }
}




//public class PickeableWeapon : IInteractable
//{
//    public bool CanInteract()
//    {
//        throw new System.NotImplementedException();
//    }

//    public void Interact()
//    {
//        throw new System.NotImplementedException();
//    }
//}