using System.Collections;
using System.Collections.Generic;
using _2048Figure.Architecture.ServiceLocator;
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
            SceneManager.LoadScene("SampleScene");
            SceneManager.LoadScene("AchivementsScene", new LoadSceneParameters(LoadSceneMode.Additive));
        }

        
    }
}
