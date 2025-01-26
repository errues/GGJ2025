using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "VfxLibrary", menuName = "ScriptableObjects/VfxLibrary", order = 1)]
public class VfxLibrary : ScriptableObject
{
    [SerializeField]
    public VfxPair[] VfxCollection;


    public GameObject GetVfxById(string id)
    {
        return VfxCollection.First(pair => pair.Id == id).Vfx;
    }

    [Serializable]
    public struct VfxPair
    {
        public string Id;
        public GameObject Vfx;
    }

}



