using UnityEngine;
using UnityEngine.UI;

public class DoneChecker : MonoBehaviour
{
    private void OnEnable()
    {
        
        for (int i = 0; i < transform.childCount; i++)
        {
            if (AchievementsManager.DoneAchievements.Contains(transform.GetChild(i).name))
            {
                transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color(1,1,1,1);
                continue;
            }
        }
    }
}
