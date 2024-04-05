using DrakraVParke.Architecture.Menu;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    [SerializeField] private SkinSelector _skinSelector;

    
    

    public void StartDefaultMode()
    {
        if (DataMenu.AvailableSkins.Contains(_skinSelector.GetSkinName()))
        {
            DataMenu._1hp = false;
            SceneManager.LoadScene("SampleScene");
            SceneManager.LoadScene("AchivementsScene", new LoadSceneParameters(LoadSceneMode.Additive));
        }
    }
    
    public void StartOneHPMode()
    {
        if (DataMenu.AvailableSkins.Contains(_skinSelector.GetSkinName()))
        {
            DataMenu._1hp = true;
            SceneManager.LoadScene("SampleScene");
            SceneManager.LoadScene("AchivementsScene", new LoadSceneParameters(LoadSceneMode.Additive));
        }
    }
}
