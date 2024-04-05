using InstantGamesBridge;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
   private void Awake()
   {
       Application.runInBackground = false;
       Bridge.advertisement.ShowBanner();
       SceneManager.LoadScene("MainMenu");
       SceneManager.LoadScene("AchivementsScene", new LoadSceneParameters(LoadSceneMode.Additive));
   }
}
