using TMPro;
using UnityEngine;

public class StatsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] stats;
    [SerializeField] private TimerView _timer;
    private void OnEnable()
    {
        int killscount = AchievementsManager.PigeonKillsCount + AchievementsManager.RatKillsCount;

        stats[0].text = "Текущее время: " + _timer.GetTime();
        stats[1].text = "ВСЕГО убийств: " + killscount;
        stats[2].text = "Убийств голубей: " + AchievementsManager.PigeonKillsCount;
        stats[3].text = "Убийств крыс: " + AchievementsManager.RatKillsCount;
    }
}
