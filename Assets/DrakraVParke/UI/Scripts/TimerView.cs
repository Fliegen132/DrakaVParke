using DrakraVParke.Player;
using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Tutorial _tutorial;
    private static string timeString;

    private void Start()
    {
        timeText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(!_tutorial.isDone)
            return;
        if(!timeText.gameObject.activeInHierarchy)
            timeText.gameObject.SetActive(true);
        AchievementsManager.Time += Time.deltaTime;
        int minutes = Mathf.FloorToInt(AchievementsManager.Time / 60);
        int seconds = Mathf.FloorToInt(AchievementsManager.Time % 60);
        timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeText.text = timeString;
    }

    public static void CheckAchievements()
    {
        AchievementsManager.CheckTime();
    }

    public string GetTime() => timeString;
}
