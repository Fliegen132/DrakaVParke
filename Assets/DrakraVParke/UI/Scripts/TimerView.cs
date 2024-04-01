using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    public static float time;
    private static string timeString;
    private void Update()
    {
        time += Time.deltaTime;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeText.text = timeString;
    }

    public static void CheckAchievements()
    {
        Debug.Log("TIIIMER");
        AchievementsManager.CheckTime(time);
    }

    public string GetTime() => timeString;
}
