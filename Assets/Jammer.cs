using UnityEngine;

public class Jammer : Cleanable
{
    [SerializeField] private Animator _animator;

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


    private void Start()
    {
        float[] values = new float[] { 0, 0.33f, 0.66f, 1 };
        int rnd = Random.Range(0, values.Length);
        _animator.SetFloat("Idle", rnd);
    }
}
