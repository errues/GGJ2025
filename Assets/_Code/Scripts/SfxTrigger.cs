using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SfxTrigger : MonoBehaviour
{
    private SfxLibrary _library;
    private AudioSource _aSource;

    private void Awake()
    {
        _aSource = GetComponent<AudioSource>();
        _aSource.playOnAwake = false;
        _aSource.loop = false;

        _library = Resources.Load<SfxLibrary>("SfxLibrary");
    }

    public void PlayFX(string id)
    {
        _aSource.pitch = 1;
        var clip = _library.GetSfxById(id);
        if (clip != null)
        {
            _aSource.PlayOneShot(clip);
        }
    }


    public void PlayFX_RandomizePitch(string id)
    {
        var clip = _library.GetSfxById(id);
        if (clip != null)
        {
            _aSource.pitch = Random.Range(1f, 3f);
            _aSource.PlayOneShot(clip);
        }
    }
}