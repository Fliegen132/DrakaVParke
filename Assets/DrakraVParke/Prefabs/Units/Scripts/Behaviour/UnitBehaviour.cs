using System;
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
        public Action @Action;
        public string Name;

        protected bool dead = false;
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
            dead = true;
            //dead
        }
        
        public abstract void UnitUpdate();
        
        public abstract void UnitFixedUpdate();
        public int GetHP() => m_hp;
    }
}