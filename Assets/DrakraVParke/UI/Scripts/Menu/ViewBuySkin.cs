using System.Linq;
using DrakraVParke.Architecture.Menu;
using InstantGamesBridge;
using UnityEngine;

public class ViewBuySkin : MonoBehaviour
{
   [SerializeField] private GameObject _windowBuy;
   [SerializeField] private ViewCoins _viewCoins;
   [SerializeField] private SkinSelector _skinSelector;
   private string _skinName;
   public void Buy()
   {
      if (!DataMenu.AvailableSkins.Contains(_skinName))
      {
         bool can = _viewCoins.Buy(300);
         if (can)
         {
            var name = _skinName.Split('_').ToArray();
            DataMenu.AvailableSkins.Add(_skinName);
            _skinSelector.UpdateSkins();
            
            Save();
            if (name[0] == "cat")
            {
               AchievementsManager.CheckCatCount(_skinSelector.catSkins.Length);
            }
            else if (name[0] == "Babushka")
            {
               AchievementsManager.CheckGrannyCount(_skinSelector.babushkaSkins.Length);
            }
            
            AchievementsManager.OpenSkin(name[0]);
         }
      }
   }

   private void Save()
   {
      Bridge.storage.Set($"money", CoinsModel.GetCoinsCount(), OnStorageSetCompleted);
      for (int i = 0; i < DataMenu.AvailableSkins.Count; i++)
      {
         Bridge.storage.Set($"skin{i}", DataMenu.AvailableSkins[i], OnStorageSetCompleted);
      }
      Bridge.storage.Set($"skinCount", DataMenu.AvailableSkins.Count, OnStorageSetCompleted);
   }

   private void OnStorageSetCompleted(bool success)
   {
      Debug.Log($"OnStorageSetCompleted, success: {success}");
   }
   
   public void OpenShop()
   {
      _windowBuy.SetActive(true);
   }

   public void SetNameSkin(string skinName)
   {
      _skinName = skinName;
   }
}
