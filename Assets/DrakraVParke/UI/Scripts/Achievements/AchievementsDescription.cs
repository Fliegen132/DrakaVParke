using System.Reflection;
using _2048Figure.Architecture.ServiceLocator;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsDescription : MonoBehaviour, IService
{
    [SerializeField] private GameObject window;
    [SerializeField] private TextMeshProUGUI textDescription;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI statsText;
    private bool m_check;
    public void SetText(string achievementName)
    {
        textName.text = achievementName;
        textDescription.text = AchievementsManager.Description[achievementName];
    }

    public void SetIcon(string iconName)
    {
        image.sprite = AchievementsManager.GetSpriteView(iconName);
        image.SetNativeSize();
        window.SetActive(true);
        if (AchievementsManager.DoneAchievements.Contains(iconName))
        {
            m_check = false;
            statsText.text = "Выполнено";
        }
        else
        {
            m_check = true;
        }
    }
    public void SetStats(string statsName)
    {
        if(!m_check)
            return;
        string[] a = statsName.Split(' ');
        Debug.Log(a[0]);
        PropertyInfo pi = typeof(AchievementsManager).GetProperty(a[0]);
        if(pi != null)
        {
            Debug.Log(a[0]);
            object value = pi.GetValue(null, null);
            statsText.text = $"{value}/{a[1]}";
        }
        else
        {
            statsText.text = "";
        }
    }
}
