using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponModel Model;
    public Animator Animator;    


    public void Attack()
    {
        Animator.SetTrigger("Attack");
    }

    public void Hide()
    {
        Animator.SetTrigger("Hide");
    }

    internal void Show()
    {
        Animator.gameObject.SetActive(true);
    }
}