using System;
using System.Collections.Generic;
using System.Linq;
using InstantGamesBridge;
using UnityEngine;

public static class AchievementsManager 
{
    public static List<string> DoneAchievements = new List<string>();

    #region statsWholeGame
    public static int RatKillsCount;
    public static int PigeonKillsCount;
    
    public static int GrannySkinsCount;
    public static int CatSkinsCount;
    
    public static int AdCount;
    public static int KnifeCount;
    #endregion

    #region statsInCurrentGame
    private static int JumpCount;
    private static int SitDownCount;
    
    #endregion
    
    
    static Sprite[] Sprites = Resources.LoadAll<Sprite>("achievementsNoBG-01");

    public static Dictionary<string, string> Description = new Dictionary<string, string>
    {
        { "Камикадзе", "Порази 1 врага и умри сам"},
        { "Новичок", "Порази 10 врагов" },
        { "Время деньги 1", "Продержись 1 минуту" },
        { "Попрыгун", "Соверши 20 прыжков за один бой" },
        { "Бывалый", "Порази 30 врагов" },
        { "Любитель крыс", "Порази 10 крыс" },

        { "Любитель голубей", "Порази 10 голубей" },
        { "Зарядка", "Присядь 25 раз за один бой" },
        { "Бабушка одобряет", "Получи первый скин Бабушки" },
        { "Опытный", "Порази 60 врагов" },
        { "Симметричный ответ 15", "Брось 15 ножей" },
        { "Время деньги 6", "Продержись 6 минут" },

        { "Равнодушен к голубям", "Порази 50 голубей" },
        { "Котик одобряет", "Получи первый скин кота" },
        { "Кинозал 5", "Посмотри 5 реклам" },
        { "Безразличен к крысам", "Порази 50 крыс" },
        { "Симметричный ответ 30", "Брось 30 ножей" },
        { "Модник", "Открой 5 скинов" },

        { "Бабушкин любимчик", "Открой все скины Бабушки" },
        { "Кошатник", "Открой все скины Котика" },
        { "Время деньги 15", "Продержись 15 минут" },
        { "Заводчик крыс", "Порази 100 крыс" },
        { "Заводчик голубей", "Порази 100 голубей" },
        { "Кинозал 20", "Посмотри 20 реклам" },

        { "Симметричный ответ 60", "Брось 60 ножей" },
        { "Кутюрье", "Открой 10 скинов" },
        { "Крысобой", "Порази 1000 крыс" },
        { "Голубененавистник", "Порази 1000 голубей" },
        { "Непобедимый", "Продержись 30 минут" },
        { "Симметричный ответ 120", "Брось 120 ножей" }
    };

    private static int i;
    public static void Init()
    {
        Bridge.storage.Get("i", IOnStorageGetCompleted);
        if (i > 0)
        {
            for (int j = 0; j < i; j++)
            {
                Bridge.storage.Get($"achievement{j}", AcOnStorageGetCompleted);  
            }
        }
        GetData("RatKillsCount", (success, data) =>
        {
            if (success && data != null)
            {
                RatKillsCount = Int32.Parse(data);
            }
        });
        
        GetData("PigeonKillsCount", (success, data) =>
        {
            if (success && data != null)
            {
                PigeonKillsCount = Int32.Parse(data);
            }
        });
        GetData("GrannySkinsCount", (success, data) =>
        {
            if (success && data != null)
            {
                GrannySkinsCount = Int32.Parse(data);
            }
        });
        GetData("CatSkinsCount", (success, data) =>
        {
            if (success && data != null)
            {
                CatSkinsCount = Int32.Parse(data);
            }
        });
        GetData("AdCount", (success, data) =>
        {
            if (success && data != null)
            {
                AdCount = Int32.Parse(data);
            }
        });
        GetData("KnifeCount", (success, data) =>
        {
            if (success && data != null)
            {
                KnifeCount = Int32.Parse(data);
            }
        });
    }
    
    private static void GetData(string key, Action<bool, string> callback)
    {
        Bridge.storage.Get(key, (success, data) =>
        {
            callback(success, data);
        });
    }

    private static void IOnStorageGetCompleted(bool success, string data)
    {
        if (success)
        {
            if (data != null) {
               i = Int32.Parse(data);
            }
        }
    }
    
    private static void AcOnStorageGetCompleted(bool success, string data)
    {
        if (success)
        {
            if (data != null) {
                DoneAchievements.Add(data);
            }
        }
    }

    public static void EndGame()
    {
        JumpCount = 0;
        SitDownCount = 0;
        
        
    }

    public static void IncreaseKillCount(string name)
    {
        if (name == "Pigeon")
        {
            PigeonKillsCount++;
        }
        else if(name == "Rat")
        {
            RatKillsCount++;
        }

        if (PigeonKillsCount >= 10 && !DoneAchievements.Contains("PigeonLover10"))
        {
            AchievementsView.ViewAchievement(GetSprite("PigeonLover10"), "Любитель голубей");
        }
        if (PigeonKillsCount >= 50 && !DoneAchievements.Contains("PigeonLover50"))
        {
            AchievementsView.ViewAchievement(GetSprite("PigeonLover50"), "Равнодушен к голубям");
        }
        if (PigeonKillsCount >= 100 && !DoneAchievements.Contains("PigeonLover100"))
        {
            AchievementsView.ViewAchievement(GetSprite("PigeonLover100"), "Заводчик голубей");
        }
        if (PigeonKillsCount >= 1000 && !DoneAchievements.Contains("PigeonLover1000"))
        {
            AchievementsView.ViewAchievement(GetSprite("PigeonLover1000"), "Голубененавистник");
        }
        
        
        if (RatKillsCount >= 10 && !DoneAchievements.Contains("RatLover10"))
        {
            AchievementsView.ViewAchievement(GetSprite("RatLover10"), "Любитель крыс");
        }
        if (RatKillsCount >= 50 && !DoneAchievements.Contains("RatLover50"))
        {
            AchievementsView.ViewAchievement(GetSprite("RatLover50"), "Безразличен к крысам");
        }
        if (RatKillsCount >= 100 && !DoneAchievements.Contains("RatLover100"))
        {
            AchievementsView.ViewAchievement(GetSprite("RatLover100"), "Заводчик крыс");
        }
        if (RatKillsCount >= 1000  && !DoneAchievements.Contains("RatLover1000"))
        {
            AchievementsView.ViewAchievement(GetSprite("RatLover1000"), "Крысобой - порази");
        }
        
        
        if (RatKillsCount + PigeonKillsCount >= 10 && !DoneAchievements.Contains("Newbie"))
        {
            AchievementsView.ViewAchievement(GetSprite("Newbie"), "Новичок");
        }
        if (RatKillsCount + PigeonKillsCount >= 30 && !DoneAchievements.Contains("Experienced1"))
        {
            AchievementsView.ViewAchievement(GetSprite("Experienced1"), "Бывалый");
        }
        if (RatKillsCount + PigeonKillsCount >= 60 && !DoneAchievements.Contains("Experienced2"))
        {
            AchievementsView.ViewAchievement(GetSprite("Experienced2"), "Опытный");
        }
    }

    public static void CheckSitDown()
    {
        SitDownCount++;
        if (SitDownCount >= 25 && !DoneAchievements.Contains("Exercise"))
        {
            AchievementsView.ViewAchievement(GetSprite("Exercise"), "Зарядка");
        }
    }

    public static void CheckJump()
    {
        JumpCount++;
        if (JumpCount >= 20 && !DoneAchievements.Contains("Jumper"))
        {
            AchievementsView.ViewAchievement(GetSprite("Jumper"), "Попрыгун");
        }
    }
    
    public static void OpenSkin(string name)
    {
        if (name.Contains("cat") && !DoneAchievements.Contains("CatLike"))
        {
            AchievementsView.ViewAchievement(GetSprite("CatLike"), "Котик одобряет");
        }
        else if(name.Contains("Babushka") && !DoneAchievements.Contains("GrannyLike"))
        { 
            AchievementsView.ViewAchievement(GetSprite("GrannyLike"), "Бабушка одобряет");
        }

        if (GrannySkinsCount + CatSkinsCount >= 5 && !DoneAchievements.Contains("Couturier5"))
        {
            AchievementsView.ViewAchievement(GetSprite("Couturier5"), "Модник");
        }
        if (GrannySkinsCount + CatSkinsCount >= 10 && !DoneAchievements.Contains("Couturier10"))
        {
            AchievementsView.ViewAchievement(GetSprite("Couturier10"), "Кутюрье");
        }
    }

    public static void WatchAd()
    {
        AdCount++;
        if (AdCount >= 5 && !DoneAchievements.Contains("CinemaHall5"))
        {
            AchievementsView.ViewAchievement(GetSprite("CinemaHall5"), "Кинозал");
        }
        if (AdCount == 20 && !DoneAchievements.Contains("CinemaHall20"))
        {
            AchievementsView.ViewAchievement(GetSprite("CinemaHall20"), "Кинозал");
        }
    }

    public static void DropKnife()
    {
        KnifeCount++;
        if (KnifeCount >= 10 && !DoneAchievements.Contains("SymmetricalAnswer15"))
        {
            AchievementsView.ViewAchievement(GetSprite("SymmetricalAnswer15"), "Симметричный ответ");
        }
        if (KnifeCount >= 30 && !DoneAchievements.Contains("SymmetricalAnswer30"))
        {
            AchievementsView.ViewAchievement(GetSprite("SymmetricalAnswer30"), "Симметричный ответ");
        }
        if (KnifeCount >= 60 && !DoneAchievements.Contains("SymmetricalAnswer60"))
        {
            AchievementsView.ViewAchievement(GetSprite("SymmetricalAnswer60"), "Симметричный ответ");
        }
        if (KnifeCount >= 120 && !DoneAchievements.Contains("SymmetricalAnswer120"))
        {
            AchievementsView.ViewAchievement(GetSprite("SymmetricalAnswer120"), "Симметричный ответ");
        }
    }

    public static void CheckGrannyCount(int count)
    {
        GrannySkinsCount++;
        if (GrannySkinsCount+1 == count && !DoneAchievements.Contains("GrannyLover"))
        {
            AchievementsView.ViewAchievement(GetSprite("GrannyLover"), "Бабушкин любимчик");
        }
    }
    
    public static void CheckCatCount(int count)
    {
        CatSkinsCount++;
        if (CatSkinsCount+1 == count && !DoneAchievements.Contains("CatLover"))
        {
            AchievementsView.ViewAchievement(GetSprite("CatLover"), "Кошатник");
        }
    }

    public static void DoneKamikaze()
    {
        if(!DoneAchievements.Contains("kamikaze"))
            AchievementsView.ViewAchievement(GetSprite("kamikaze"), "Камикадзе");
    }

    public static void CheckTime(float time)
    {
        if (time >= 10 && !DoneAchievements.Contains("MoneyTime1"))
        {
            AchievementsView.ViewAchievement(GetSprite("MoneyTime1"), "Время деньги");
        }
        
        if (time >= 360 && !DoneAchievements.Contains("MoneyTime6"))
        {
            AchievementsView.ViewAchievement(GetSprite("MoneyTime6"), "Время деньги");
        }
        
        if (time >= 900 && !DoneAchievements.Contains("MoneyTime15"))
        {
            AchievementsView.ViewAchievement(GetSprite("MoneyTime15"), "Время деньги");
        }
        
        if (time >= 1800 && !DoneAchievements.Contains("MoneyTime30"))
        {
            AchievementsView.ViewAchievement(GetSprite("MoneyTime30"), "Непобедимый");
        }
    }


    public static Sprite GetSprite(string spriteName)
    {
        var sprite = Sprites.Where(a => a.name == spriteName).First();
        DoneAchievements.Add(spriteName);
        return sprite;
    }
}
