using TMPro;
using UnityEngine;

public class StatsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] stats;
    [SerializeField] private TimerView _timer;

    private int _pigeonKills;
    private int _ratKills;
    private int _knifeCount;
    public void Init()
    {
        _pigeonKills = AchievementsManager.PigeonKillsCount;
        _ratKills = AchievementsManager.RatKillsCount;
        _knifeCount = AchievementsManager.KnifeCount;
    }

    private void OnEnable()
    {
        stats[0].text = "Текущее время: " + _timer.GetTime();
        stats[1].text = $"Количество убийств: {(AchievementsManager.PigeonKillsCount-_pigeonKills) + ( AchievementsManager.RatKillsCount- _ratKills)} ";
        stats[2].text = $"Убийств голубей: {AchievementsManager.PigeonKillsCount- _pigeonKills}" ;
        stats[3].text = $"Убийств крыс: { AchievementsManager.RatKillsCount-_ratKills}";
        stats[4].text = $"Ножей кинуто: {AchievementsManager.KnifeCount - _knifeCount }";
    }
}
