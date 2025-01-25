using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleanable : MonoBehaviour, IInteractable
{
    [SerializeField] private CleanableModel _model;
    [SerializeField] private WeaponModel _requiredWeapon;
    [Space]
    [SerializeField] private Renderer _renderer;

    private Dictionary<int, Color> _colorBindings = new();

    private CharacterWeaponHandler _weaponHandler;
    private Coroutine _dirtUpdateCoroutine;

    private int _dirtLevel = 2; // 0 = clean, 2 = dirty
    private const int MIN_DIRT_LEVEL = 0;
    private const int MAX_DIRT_LEVEL = 2;


    private void Awake()
    {
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

    private void ReduceDirtLevel()
    {
        _dirtLevel = Math.Clamp(_dirtLevel - 1, MIN_DIRT_LEVEL, MAX_DIRT_LEVEL);
        UpdateView();
    }

    private void UpdateView()
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

    private void IncreaseDirtLevel()
    {
        if (_dirtLevel < MAX_DIRT_LEVEL)
        {
            _dirtLevel = Math.Clamp(_dirtLevel + 1, MIN_DIRT_LEVEL, MAX_DIRT_LEVEL);
            UpdateView();
        }
    }
}
