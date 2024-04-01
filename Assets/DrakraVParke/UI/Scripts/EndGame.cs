using System.Collections;
using System.Collections.Generic;
using _2048Figure.Architecture.ServiceLocator;
using InstantGamesBridge;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour, IService
{
    [SerializeField] private TextMeshProUGUI _killsText;
    [SerializeField] private GameObject _endWindpow;
    [SerializeField] private GameObject _mainWindpow;
    public bool end;
    private ViewScore _viewScore;

    private void Start()
    {
        _viewScore = ServiceLocator.current.Get<ViewScore>();
    }

    public void End()
    {
        end = true;
        _mainWindpow.SetActive(false);
        _killsText.text = _viewScore._scoreText.text;
        AchievementsManager.EndGame();
        TimerView.CheckAchievements();
        TimerView.time = 0;
        if (_killsText.text == "1")
        {
            AchievementsManager.DoneKamikaze();
        }
        
        Bridge.storage.Set("RatKillsCount", AchievementsManager.RatKillsCount, OnStorageSetCompleted);
        Bridge.storage.Set("PigeonKillsCount", AchievementsManager.PigeonKillsCount, OnStorageSetCompleted);
        Bridge.storage.Set("GrannySkinsCount", AchievementsManager.GrannySkinsCount, OnStorageSetCompleted);
        Bridge.storage.Set("CatSkinsCount", AchievementsManager.CatSkinsCount, OnStorageSetCompleted);
        Bridge.storage.Set("AdCount", AchievementsManager.AdCount, OnStorageSetCompleted);
        Bridge.storage.Set("KnifeCount", AchievementsManager.KnifeCount, OnStorageSetCompleted);

        StartCoroutine(EndCoroutine());
    }
    
    private void OnStorageSetCompleted(bool success)
    {
        Debug.Log($"OnStorageSetCompleted, success: {success}");
    }

    private IEnumerator EndCoroutine()
    {
        yield return new WaitForSeconds(1f);
        _endWindpow.SetActive(true);
    }

    public void Restart()
    {
        ServiceLocator.current.UnregisterAll();
        SceneManager.LoadScene("SampleScene");
        SceneManager.LoadScene("AchivementsScene", new LoadSceneParameters(LoadSceneMode.Additive));
    }

    public void ExitInMenu()
    {
        ServiceLocator.current.UnregisterAll();
        SceneManager.LoadScene("MainMenu");
        SceneManager.LoadScene("AchivementsScene", new LoadSceneParameters(LoadSceneMode.Additive));
    }
}
