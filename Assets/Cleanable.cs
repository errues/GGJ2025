using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cleanable : Dirtable, IInteractable {
    [SerializeField] protected CleanableModel _model;
    [SerializeField] protected WeaponModel _requiredWeapon;

    private Dictionary<int, Color> _colorBindings = new();
    protected CharacterWeaponHandler _weaponHandler;
    private Coroutine _dirtUpdateCoroutine;

    protected override int MaxDirtLevel => 2;

    protected virtual void Awake() {
        _dirtLevel = 0;
        _weaponHandler = FindFirstObjectByType<CharacterWeaponHandler>();
        UpdateView();
    }

    public abstract bool CanInteract();

    public virtual void Interact() {
        ReduceDirtLevel();
    }


    protected override void UpdateView() {
        if (_dirtUpdateCoroutine != null) {
            StopCoroutine(_dirtUpdateCoroutine);
            _dirtUpdateCoroutine = null;
        }

        _dirtUpdateCoroutine = StartCoroutine(DirtUpdateCoroutine());
    }

    private IEnumerator DirtUpdateCoroutine() {
        yield return new WaitForSeconds(Random.Range(_model.RandomDirtTimeLimits.x, _model.RandomDirtTimeLimits.y));
        IncreaseDirtLevel();
        yield return null;
    }


    public void CancelInteract() { }

    public void EnteredInteractionRange() { }
}
