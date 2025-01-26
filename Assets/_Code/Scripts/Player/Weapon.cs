using System;
using System.Collections;
using UnityEngine;

public class Weapon : Dirtable
{
    public WeaponModel Model;
    public Animator Animator;
    [Space]
    public GameObject MidDirtyGO;
    public GameObject FullDirtyGO;

    protected override int MaxDirtLevel => Model.MaxDirtLevel;

    private void Start()
    {
        _dirtLevel = 0;
    }

    public void Attack()
    {
        Animator.SetTrigger("Attack");
        IncreaseDirtLevel();
    }

    public void Hide()
    {
        Animator.SetTrigger("Hide");
    }

    protected override void UpdateView()
    {
        if (MidDirtyGO == null || FullDirtyGO == null) return;

        float threshold = Model.MaxDirtLevel / 3;
        if (_dirtLevel <= threshold)
        {
            MidDirtyGO.SetActive(false);
            FullDirtyGO.SetActive(false);
        }
        else if (_dirtLevel > threshold && _dirtLevel < threshold * 2)
        {
            MidDirtyGO.SetActive(true);
            FullDirtyGO.SetActive(false);
        }
        else
        {
            MidDirtyGO.SetActive(false);
            FullDirtyGO.SetActive(true);
        }
    }

    internal void Show()
    {
        Animator.gameObject.SetActive(true);
    }
}