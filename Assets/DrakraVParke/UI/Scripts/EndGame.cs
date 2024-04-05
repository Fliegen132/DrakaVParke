using System.Collections;
using System.Collections.Generic;
using _2048Figure.Architecture.ServiceLocator;
using DrakraVParke.Architecture.Menu;
using InstantGamesBridge;
using InstantGamesBridge.Modules.Advertisement;
using InstantGamesBridge.Modules.Storage;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour, IService
{
    [SerializeField] private TextMeshProUGUI _killsText;
    [SerializeField] private GameObject _endWindpow;
    [SerializeField] private GameObject _mainWindpow;
    [SerializeField] private AudioMixerGroup _mixer;

    public bool end;
    private ViewScore _viewScore;
    
    private void Start()
    {
        _viewScore = ServiceLocator.current.Get<ViewScore>();
        Bridge.advertisement.interstitialStateChanged += OnInterstitialStateChanged;
        Bridge.advertisement.SetMinimumDelayBetweenInterstitial(60);
    }

    public void End()
    {
        _mainWindpow.SetActive(false);
        _killsText.text = _viewScore._scoreText.text;
        if (_killsText.text == "1")
        {
            AchievementsManager.DoneKamikaze();
        }
        if (!end)
        {
            PlayerPrefs.SetInt("RatKillsCount", AchievementsManager.RatKillsCount);
            PlayerPrefs.SetInt("PigeonKillsCount", AchievementsManager.PigeonKillsCount);
            
            PlayerPrefs.SetInt("KnifeCount", AchievementsManager.KnifeCount);
        }
        TimerView.CheckAchievements();
        end = true;
        StartCoroutine(EndCoroutine());
    }

    private IEnumerator EndCoroutine()
    {
        yield return new WaitForSeconds(1f);
        _endWindpow.SetActive(true);
        DataMenu.CurrentTry++;
        if (DataMenu.CurrentTry >= 2)
        {
            Bridge.advertisement.ShowInterstitial(true);
            DataMenu.CurrentTry = 0;
        }
    }
    
    private void OnInterstitialStateChanged(InterstitialState state)
    {
        if (state == InterstitialState.Opened)
        {
            _mixer.audioMixer.SetFloat("Music", -80);
        }
        if (state == InterstitialState.Closed || state == InterstitialState.Failed)
        {
            _mixer.audioMixer.SetFloat("Music", 0);
        }


        Debug.Log(state);
    }

    public void Restart()
    {
        AchievementsManager.EndGame();
        ServiceLocator.current.UnregisterAll();
        SceneManager.LoadSceneAsync("SampleScene");
        SceneManager.LoadScene("AchivementsScene", new LoadSceneParameters(LoadSceneMode.Additive));
    }

    public void ExitInMenu()
    {
        AchievementsManager.EndGame();
        ServiceLocator.current.UnregisterAll();
        SceneManager.LoadSceneAsync("BootstrapScene");
        SceneManager.LoadScene("AchivementsScene", new LoadSceneParameters(LoadSceneMode.Additive));
    }
}
