using _2048Figure.Architecture.ServiceLocator;
using DrakaVParke.Architecture;
using DrakraVParke.Architecture.Menu;
using UnityEngine;

namespace DrakraVParke.Units
{
    public enum DamageType
    {
        Default = 0,
        Hard = 1,
        SuperHard = 2
    }

    public abstract class UnitBehaviour
    {
        private int m_hp;
        protected GameObject unit;
        public string Name;

        public bool dead = false;

        private ViewModelScore _viewModelScore;
        public UnitBehaviour()
        {
            _viewModelScore = ServiceLocator.current.Get<ViewModelScore>();
        }

        public virtual void Init()
        {
            
        }

        public virtual void TakeDamage(int damage, DamageType type)
        {
            m_hp -= damage;
            if(m_hp <= 0)
                Dead();    
        }

        public void SetHp(int newHP)
        {
            m_hp = newHP;
        }

        protected virtual void Dead()
        {
            _viewModelScore.UpdateScore();
            AchievementsManager.IncreaseKillCount(Name);
        }
        
        public void AddHP(int newHP)
        {
            if(DataMenu._1hp)
                return;
            m_hp += newHP;
            if (m_hp >= 10)
            {
                m_hp = 10;
            }
            Debug.Log(m_hp);
        }
        
        public abstract void UnitUpdate();
        
        public abstract void UnitFixedUpdate();
        public int GetHP() => m_hp;

        
    }
}