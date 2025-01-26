using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SfxLibrary", menuName = "ScriptableObjects/SfxLibrary", order = 1)]
public class SfxLibrary : ScriptableObject
{
    [SerializeField]
    public SfxPair[] SfxCollection;


    public AudioClip GetSfxById(string id)
    {
        return SfxCollection.First(pair => pair.Id == id).Sfx;
    }

    [Serializable]
    public struct SfxPair
    {
        public string Id;
        public AudioClip Sfx;
    }
}
