using UnityEngine;

public class DoneChecker : MonoBehaviour
{
    private void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (AchievementsManager.DoneAchievements.Contains(transform.GetChild(i).name))
            {
                transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
