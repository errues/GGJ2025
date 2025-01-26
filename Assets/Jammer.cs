using System.Collections.Generic;
using UnityEngine;

public class Jammer : Cleanable
{
    [SerializeField] private Animator _animator;

    public GameObject MidDirtyGO;
    public GameObject FullDirtyGO;
    private int threshold;


    public override bool CanInteract()
    {
        return true;
    }

    public override void Interact()
    {
        if (_weaponHandler.Current.Model == _requiredWeapon)
        {
            ReduceDirtLevel();
            if (_dirtLevel == MinDirtLevel)
            {
                _animator.SetTrigger("Cleaned");
            }
            else
            {
                _animator.ResetTrigger("Cleaning");
                _animator.SetTrigger("Cleaning");
            }
        }
        else
        {
            int rnd = Random.Range(1, 3);
            _animator.SetTrigger($"Hit{rnd}");
        }
    }


    protected override void UpdateView()
    {
        if (MidDirtyGO == null || FullDirtyGO == null) return;

        if (_dirtLevel == 0)
        {
            MidDirtyGO.SetActive(false);
            FullDirtyGO.SetActive(false);
        }
        else if (_dirtLevel == 1)
        {
            MidDirtyGO.SetActive(true);
            FullDirtyGO.SetActive(false);
        }
        else // max
        {
            MidDirtyGO.SetActive(true);
            FullDirtyGO.SetActive(true);
        }

        base.UpdateView();
    }



    private void Start()
    {
        float[] values = new float[] { 0, 0.34f, 0.67f, 1 };
        int rnd = Random.Range(0, values.Length);
        _animator.SetFloat("Idle", rnd);
    }
}
