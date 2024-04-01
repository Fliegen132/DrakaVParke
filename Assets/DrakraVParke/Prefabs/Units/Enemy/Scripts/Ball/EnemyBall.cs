using DrakaVParke.Architecture;
using UnityEngine;
using UnityEngine.UIElements;

namespace DrakraVParke.Units.Ball
{
    public class EnemyBall : UnitBehaviour
    {
        private Transform _target;
        private bool _haveGun;
        private float _direct;
        public EnemyBall(GameObject enemy, string name)
        {
            Name = name;
            unit = enemy;
            _target = UnitList.Player.transform;
        }

        public override void Init()
        {
            unit.GetComponent<Animator>().Play("RatBall");
            unit.transform.localPosition = new Vector3(0,0,0);
            dead = false;
            SetHp(1);
            float targetX = _target.transform.position.x;
            if (unit.transform.position.x > targetX)
            {
                _direct = -1;
            }
            else
            {
                _direct = 1;
            }
            Debug.Log(unit.transform.position.x);
            Debug.Log(_direct);
            unit.transform.localScale = new Vector3(_direct, 1, 1);
        }
     
        public override void TakeDamage(int damage, DamageType type)
        {
            var effect = Resources.Load<GameObject>("KickSplash");
            Vector2 vector2 = new Vector2(unit.transform.position.x + 0.2f, unit.transform.position.y + 0.6f);
            Object.Instantiate(effect, vector2, Quaternion.identity);
            Dead();
        }

        public override void UnitUpdate()
        {
            
        }

        public override void UnitFixedUpdate()
        {
            
        }

        protected override void Dead()
        {
            base.Dead();
            dead = true;
            unit.transform.SetParent(null);
            unit.GetComponent<Rigidbody2D>().gravityScale = 1;
            unit.GetComponent<BoxCollider2D>().enabled = false;
            unit.GetComponent<GunHandler>()?.KickOut();
            unit.GetComponent<Animator>().Play("RatDead");
            
        }
    }
}