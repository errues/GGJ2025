using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Weapon : Dirtable {
    public WeaponModel Model;
    public Animator Animator;
    [Space]
    public GameObject MidDirtyGO;
    public GameObject FullDirtyGO;
    private int threshold;

    private bool _isAttacking;

    protected override int MaxDirtLevel => Model.MaxDirtLevel;

    private void Start() {
        _dirtLevel = 0;
    }

    public async virtual void Attack() {
        if (_isAttacking) return;

        _isAttacking = true;
        ShowFX();
        int rnd = Random.Range(1, 6);
        Animator.SetInteger("Attack_Random", rnd);
        await Task.Delay(50);
        Animator.SetInteger("Attack_Random", 0);
        ShowFX();
        _isAttacking = false;
    }

    public void Hide() {
        Animator.SetTrigger("Hide");
    }

    [ContextMenu("Clean")]
    public void Clean() {
        _dirtLevel = MinDirtLevel;
        UpdateView();
    }

    protected override void UpdateView() {
        if (MidDirtyGO == null || FullDirtyGO == null) return;

        threshold = Model.MaxDirtLevel / 3;
        if (_dirtLevel <= threshold) {
            MidDirtyGO.SetActive(false);
            FullDirtyGO.SetActive(false);
        } else if (_dirtLevel > threshold && _dirtLevel <= threshold * 2) {
            MidDirtyGO.SetActive(true);
            FullDirtyGO.SetActive(false);
        } else {
            MidDirtyGO.SetActive(false);
            FullDirtyGO.SetActive(true);
        }
    }

    internal void Show() {
        Animator.gameObject.SetActive(true);
    }

    private void ShowFX()
    {
        if (Model.name.Equals("Mop"))
        {
            ParticleSystem particleFX = GetComponentInChildren<ParticleSystem>();
            if (particleFX != null)
            {
                particleFX.Play();
            }
        }
    }
}