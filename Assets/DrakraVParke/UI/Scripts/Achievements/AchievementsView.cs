using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsView : MonoBehaviour
{
    private static Animator[] _anim;
    private static TextMeshProUGUI[] _text;
    private static Image[] _image;
    private static int currentView;
    private static AchievementsView _instance;

    private void Awake()
    {
        _instance = this;
        int childCount = transform.childCount;
        _anim = new Animator[childCount];
        _text = new TextMeshProUGUI[childCount];
        _image = new Image[childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            _anim[i] = transform.GetChild(i).GetComponent<Animator>();
            _text[i] = _anim[i].gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            _image[i] = _anim[i].gameObject.transform.GetChild(0).GetComponent<Image>();
        }

        AchievementsManager.Init();
        CoinsModel.Init();
        for (int i = 0; i < AchievementsManager.DoneAchievements.Count; i++)
        {
            Debug.Log(AchievementsManager.DoneAchievements[i]);
        }
    }

    public static void ViewAchievement(Sprite icon, string text)
    {
        _anim[currentView].Play("Open");
        _text[currentView].text = text;
        _image[currentView].sprite = icon;
        currentView++;
        _instance.StartCoroutine(_instance.ResetCurrentView());
        CoinsModel.AddCoins(100);
        FindObjectOfType<ViewCoins>()?.UpdateText();
        _instance.Save();
    }

    private void Save()
    {
        for(int i =0; i < AchievementsManager.DoneAchievements.Count; i++)
        {
            PlayerPrefs.SetString($"achievement{i}", AchievementsManager.DoneAchievements[i]);
        }
        
        PlayerPrefs.SetInt("i", AchievementsManager.DoneAchievements.Count);
        PlayerPrefs.SetInt($"money", CoinsModel.GetCoinsCount());
    }

    private IEnumerator ResetCurrentView()
    {
        yield return new WaitForSeconds(2f);
        currentView = 0;
    }

}
