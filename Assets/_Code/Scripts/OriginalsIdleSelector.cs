using MyBox;
using System.Collections.Generic;
using UnityEngine;

public class OriginalsIdleSelector : MonoBehaviour
{
    [SerializeField] private int _numAnims = 4;
    [SerializeField] private Animator[] _animators;

    private void Awake()
    {
        HashSet<int> animIndex = new HashSet<int>(_numAnims);

        foreach (Animator animator in _animators)
        {
            if (animIndex.Count == 0)
            {
                FillIndexSet(ref animIndex);
            }

            int index = animIndex.GetRandom();
            animIndex.Remove(index);

            animator.SetFloat("IdleBlend", (float)index / (_numAnims - 1));
        }
    }

    private void FillIndexSet(ref HashSet<int> animIndex)
    {
        for (int i = 0; i < _numAnims; i++)
            animIndex.Add(i);
    }
}
