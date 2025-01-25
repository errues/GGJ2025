using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponModel Model;
    public Animator Animator { get; private set; }

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
    }


    public void Attack()
    {
        Animator.SetTrigger("Attack");
    }

    public IEnumerator Hide()
    {        
        Animator.SetTrigger("Hide");
        yield return new WaitForSeconds(1);
        Animator.gameObject.SetActive(false);
    }

    internal void Show()
    {
        Animator.gameObject.SetActive(true);
    }
}