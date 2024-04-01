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
    
    public void SetText(string achievementName)
    {
        textName.text = achievementName;
        textDescription.text = AchievementsManager.Description[achievementName];
    }

    public void SetIcon(string iconName)
    {
        image.sprite = AchievementsManager.GetSprite(iconName);
        image.SetNativeSize();
        window.SetActive(true);
    }

}
