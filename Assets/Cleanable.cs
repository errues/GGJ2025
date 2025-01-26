using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleanable : Dirtable, IInteractable
{
    [SerializeField] private CleanableModel _model;
    [SerializeField] private WeaponModel _requiredWeapon;
    [Space]
    [SerializeField] private Renderer _renderer;

    private Dictionary<int, Color> _colorBindings = new();

    private CharacterWeaponHandler _weaponHandler;
    private Coroutine _dirtUpdateCoroutine;

    protected override int MaxDirtLevel => 2;

    private void Awake()
    {
        _dirtLevel = 2;

        _weaponHandler = FindFirstObjectByType<CharacterWeaponHandler>();
        _colorBindings = new Dictionary<int, Color>();
        _colorBindings.Add(0, Color.white);        
        _colorBindings.Add(1, Color.grey);        
        _colorBindings.Add(2, Color.black);


        UpdateView();
    }

    public bool CanInteract()
    {
        return _weaponHandler.Current.Model == _requiredWeapon;
    }

    public void Interact()
    {
        ReduceDirtLevel();
    }



    protected override void UpdateView()
    {
        _renderer.material.SetColor("_BaseColor", _colorBindings[_dirtLevel]);

        if (_dirtUpdateCoroutine != null)
        {
            StopCoroutine(_dirtUpdateCoroutine);
            _dirtUpdateCoroutine = null;
        }

        _dirtUpdateCoroutine = StartCoroutine(DirtUpdateCoroutine());
    }

    private IEnumerator DirtUpdateCoroutine()
    {
        yield return new WaitForSeconds(_model.SecondsBetweenDirtUpgrades);
        IncreaseDirtLevel();
        yield return null;
    }



    public void CancelInteract()
    {
    }
}
