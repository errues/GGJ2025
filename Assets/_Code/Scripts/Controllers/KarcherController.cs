using MyBox;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

public class KarcherController : MonoBehaviour
{
    [SerializeField] private PlayableDirector _appear;
    [SerializeField] private PlayableDirector _appearIdle;

    [SerializeField] private PlayableDirector _usingLoop;
    [SerializeField] private PlayableDirector _stopUsing;

    private void Awake()
    {
        _appear.gameObject.SetActive(false);
        _appearIdle.gameObject.SetActive(false);

        _usingLoop.gameObject.SetActive(false);
        _stopUsing.gameObject.SetActive(false);
    }

    [ButtonMethod]
    public async void KarcherAppear()
    {
        _appear.gameObject.SetActive(true);
        _appear.Play();

        await Task.Delay((int)(_appear.duration * 1000));
        while (_appear.state != PlayState.Paused) 
        {
            await Task.Delay(20);
        }

        _appear.gameObject.SetActive(false);

        _appearIdle.gameObject.SetActive(true);
        _appearIdle.Play();
    }

    [ButtonMethod]
    public void StartUsingKarcher()
    {
        _appear.gameObject.SetActive(false);

        _appearIdle.Stop();
        _appearIdle.gameObject.SetActive(false);

        _usingLoop.gameObject.SetActive(true);
        _usingLoop.Play();
    }

    [ButtonMethod]
    public async void FinishUsingKarcher()
    {
        _usingLoop.Stop();
        await Task.Delay(100);
        _usingLoop.gameObject.SetActive(false);

        _stopUsing.gameObject.SetActive(true);
        _stopUsing.Play();


        await Task.Delay((int)(_stopUsing.duration * 1000));
        while (_stopUsing.state != PlayState.Paused)
        {
            await Task.Delay(20);
        }

        _stopUsing.gameObject.SetActive(false);
    }
}
