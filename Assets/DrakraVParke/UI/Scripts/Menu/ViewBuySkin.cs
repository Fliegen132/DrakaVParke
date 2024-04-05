using System.Collections;
using System.Linq;
using DrakraVParke.Architecture.Menu;
using InstantGamesBridge;
using InstantGamesBridge.Modules.Advertisement;
using UnityEngine;
using UnityEngine.Audio;

public class ViewBuySkin : MonoBehaviour
{
   [SerializeField] private GameObject _windowBuy;
   [SerializeField] private ViewCoins _viewCoins;
   [SerializeField] private SkinSelector _skinSelector;
   [SerializeField] private AudioMixerGroup _mixer;
   public bool WindowOpen = false;
   private bool canUpdate;
   private void Start()
   {
      Bridge.advertisement.rewardedStateChanged += null;
      Bridge.advertisement.rewardedStateChanged += OnRewardedStateChanged;
   }
   public void Buy()
   {
      if (!DataMenu.AvailableSkins.Contains(DataMenu.BuyedHeroSkin))
      {
         bool can = _viewCoins.TryBuy(300);
         if (can)
         {
            Bridge.advertisement.ShowRewarded();
         }
         else
            CloseWindow();
      }
   }

   private void OnRewardedStateChanged(RewardedState state)
   {
      if (this == null) return; 
      if (state == RewardedState.Opened)
      {
         _mixer.audioMixer.SetFloat("Music", -80);
      }
      else if (state == RewardedState.Rewarded)
      {
         Ep();
      }
      else if (state == RewardedState.Closed || state == RewardedState.Failed)
      {
         _mixer.audioMixer.SetFloat("Music", 0);
         canUpdate = false;
      }
   }

   private void Ep()
   {
      WindowOpen = false;
      _viewCoins.Buy(300);
      var NAME = DataMenu.BuyedHeroSkin.Split('_').ToList();
      Debug.Log(NAME[0]);
      if (NAME[0] == "cat")
      {
         AchievementsManager.CheckCatCount(_skinSelector.catSkins.Length);
      }
      else if (NAME[0] == "Babushka")
      {
         AchievementsManager.CheckGrannyCount(_skinSelector.babushkaSkins.Length);
      }
      DataMenu.AvailableSkins.Add(DataMenu.BuyedHeroSkin);
      _skinSelector.UpdateSkins();
      AchievementsManager.OpenSkin(NAME[0]);
      AchievementsManager.WatchAd();
      Save();
   }

   private void Save()
   {
      PlayerPrefs.SetInt("AdCount", AchievementsManager.AdCount);
      PlayerPrefs.SetInt($"money", CoinsModel.GetCoinsCount());
      for (int i = 0; i < DataMenu.AvailableSkins.Count; i++)
      {
         PlayerPrefs.SetString($"skin{i}", DataMenu.AvailableSkins[i]);
      }
      PlayerPrefs.SetInt($"skinCount", DataMenu.AvailableSkins.Count);
      PlayerPrefs.SetInt("GrannySkinsCount", AchievementsManager.GrannySkinsCount);
      PlayerPrefs.SetInt("CatSkinsCount", AchievementsManager.CatSkinsCount);
   }

   public void CloseWindow()
   {
      _windowBuy.SetActive(false);
      WindowOpen = false;
      Debug.Log(WindowOpen);
   }

   public void OpenShop()
   {
      if(DataMenu.AvailableSkins.Contains(DataMenu.BuyedHeroSkin))
         return;
      WindowOpen = true;
      _windowBuy.SetActive(true);
   }

   public void SetNameSelector(SkinSelector selector)
   {
      _skinSelector = selector;
   }
}
