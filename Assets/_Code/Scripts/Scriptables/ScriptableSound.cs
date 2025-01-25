using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableSound", menuName = "ScriptableObjects/ScriptableSound")]
public class ScriptableSound : ScriptableObject {
    [SerializeField] private List<AudioClip> clips;

    public AudioClip GetRandomClip() {
        if (clips.Count > 0) {
            return clips[Random.Range(0, clips.Count)];
        } else {
            return null;
        }
    }
}
