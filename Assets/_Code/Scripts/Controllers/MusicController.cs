using System.Collections;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController Instance { get; private set; 
    }
    public AudioSource gameMusicLoop;
    public AudioSource karcherMusicLoop;

    public float musicAttenuation = 0.4f;

    private void Awake()
    {
        Instance = this;
    }

    public void AttenuateMusic()
    {
        StartCoroutine(FadeVolume(gameMusicLoop, musicAttenuation));
    }

    public void StopMusic()
    {
        StartCoroutine(FadeVolume(gameMusicLoop, 0f, true));
    }

    public void StartKarcherMusic()
    {
        karcherMusicLoop.volume = 1f;
        karcherMusicLoop.Play();
    }

    public void ResumeMusic()
    {
        StartCoroutine(FadeVolume(karcherMusicLoop, 0f, true));

        gameMusicLoop.Play();
        StartCoroutine(FadeVolume(gameMusicLoop, 1f));
    }

    private IEnumerator FadeVolume(AudioSource source, float value, bool stop = false)
    {
        float lerpStep = 0f;
        float initialVolume = source.volume;

        while (lerpStep <= 1f)
        {
            lerpStep += 2 * Time.deltaTime;
            source.volume = Mathf.Lerp(initialVolume, value, lerpStep);

            yield return null;
        }

        if (stop)
            source.Stop();
    }
}
