using MyBox;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

public class KarcherController : MonoBehaviour {
    [SerializeField] private MinMaxFloat karcherFirstAppear = new MinMaxFloat(40, 60);
    [SerializeField] private MinMaxFloat karcherCooldownAppear = new MinMaxFloat(40, 60);
    [SerializeField] private float karcherDuration = 10f;

    [Separator]

    [SerializeField] private PlayableDirector _appear;
    [SerializeField] private PlayableDirector _appearIdle;

    [SerializeField] private PlayableDirector _usingLoop;
    [SerializeField] private PlayableDirector _stopUsing;

    private bool _canTakeKarcher;

    public static KarcherController Instance { get; private set; }

    private void Awake() {
        Instance = this;

        _appear.gameObject.SetActive(false);
        _appearIdle.gameObject.SetActive(false);

        _usingLoop.gameObject.SetActive(false);
        _stopUsing.gameObject.SetActive(false);
    }

    private IEnumerator Start() {
        yield return new WaitForSeconds(karcherFirstAppear.RandomInRange());

        while (true) {
            KarcherAppear();

            yield return new WaitUntil(() => _usingLoop.gameObject.activeSelf);

            yield return new WaitForSeconds(karcherDuration);
            yield return new WaitForSeconds(karcherCooldownAppear.RandomInRange());
        }
    }

    [ButtonMethod]
    public async void KarcherAppear() {

        MusicController.Instance.AttenuateMusic();

        _appear.gameObject.SetActive(true);
        _appear.Play();

        await Task.Delay((int)(_appear.duration * 1000));
        while (_appear.state != PlayState.Paused) {
            await Task.Delay(20);
        }

        _appear.gameObject.SetActive(false);

        _appearIdle.gameObject.SetActive(true);
        _appearIdle.Play();

        _canTakeKarcher = true;
    }

    public async void StartUsingKarcher(CharacterWeaponHandler characterWeapon) {

        MusicController.Instance.StopMusic();

        _appear.gameObject.SetActive(false);

        _appearIdle.Stop();
        _appearIdle.gameObject.SetActive(false);

        await Task.Delay((int)(3500));
        MusicController.Instance.StartKarcherMusic();

        _usingLoop.gameObject.SetActive(true);
        _usingLoop.Play();

        await Task.Delay((int)(karcherDuration * 1000));
        ((Karcher)characterWeapon.Current).DeactivateKarcher();
        FinishUsingKarcher();

        characterWeapon.FinishSpecialWeapon();
    }

    [ButtonMethod]
    public async void FinishUsingKarcher() {
        _usingLoop.Stop();
        await Task.Delay(100);
        _usingLoop.gameObject.SetActive(false);

        MusicController.Instance.ResumeMusic();
        _stopUsing.gameObject.SetActive(true);
        _stopUsing.Play();


        await Task.Delay((int)(_stopUsing.duration * 1000));
        while (_stopUsing.state != PlayState.Paused) {
            await Task.Delay(20);
        }

        _stopUsing.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other) {
        if (!_canTakeKarcher) return;
        if (other.CompareTag("Player")) {
            _canTakeKarcher = false;
            CharacterWeaponHandler characterWeapon = other.gameObject.GetComponentInChildren<CharacterWeaponHandler>();
            characterWeapon.SetSpecialWeapon();
            StartUsingKarcher(characterWeapon);
        }
    }
}
