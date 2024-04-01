using System.Threading.Tasks;
using DrakaVParke.Architecture;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace DrakraVParke.Units
{
    public class Enemy : UnitBehaviour
    {
        private Transform _target;
        private float _speed = 5;
        private float _direct;
        private bool _canMove = true;
        private GunHandler _gunHandler;
        public Enemy(string name, GameObject enemy)
        {
            Name = name;
            _target = UnitList.Player.transform;
            unit = enemy;
            unit.GetComponent<EnemySubs>()._HitDetected += HitDetected;
            unit.GetComponent<EnemySubs>()._DeadAction += SetDefault;
            unit.GetComponent<EnemySubs>()._CanMove += ContinueMove;
            _gunHandler = unit.GetComponent<GunHandler>();
            _gunHandler.SetAnim += InitWeapon;
            _gunHandler.Init();
        }
    
        public override void UnitUpdate()
        {
            Move();
        }

        private void Move()
        {
            if(!_canMove || dead)
                return;
            if (_haveGun)
            {
                unit.GetComponent<Animator>().SetBool("HaveGun", true);
            }
            else
            {
                unit.GetComponent<Animator>().SetBool("HaveGun", false);
            }
            float targetX = _target.transform.position.x;
            if (unit.transform.position.x > targetX)
            {
                _direct = -1;
            }
            else
            {
                _direct = 1;
            }
            unit.transform.localScale = new Vector3(_direct, 1, 1);

            if (unit.gameObject.activeInHierarchy)
            {
                if (unit.transform.position.x >= -1.6f && unit.transform.position.x <= 1.6f)
                {
                    Attack();
                    return;
                }
                unit.GetComponent<Animator>().SetBool("Attack", false);
                unit.GetComponent<Animator>().SetBool("AttackNoGun", false);
                Vector3 direction = new Vector3(_speed * _direct, 0);
                unit.transform.Translate(direction * Time.deltaTime);
            }
        }

        private void Attack()
        {
            if(_gunHandler._haveGun)
                unit.GetComponent<Animator>().SetBool("Attack", true);
            else
            {
                unit.GetComponent<Animator>().SetBool("AttackNoGun", true);
            }
        }
        
        private void HitDetected()
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(new Vector2(unit.transform.position.x, unit.transform.position.y + 0.5f), new Vector2(_direct, 0), 1.5f);
            if (hit != null)
            {
                foreach(var hitInfo in hit)
                {
                    Unit unit = hitInfo.collider.GetComponent<Unit>();
                    if (unit != null && unit.CompareTag("Player"))
                    {
                        unit.GetBehaviour().TakeDamage(1, DamageType.Default);
                    }
                }
            }
        }

        public override void TakeDamage(int damage, DamageType type)
        {
            _canMove = false;
            var effect = Resources.Load<GameObject>("KickSplash");
            Vector2 vector2 = new Vector2(unit.transform.position.x + 0.2f, unit.transform.position.y + 0.6f);
            
            var particle = Resources.Load<GameObject>($"{Name}Particle");
            Vector2 vector22 = new Vector2(unit.transform.position.x + 0.2f, unit.transform.position.y + 0.6f);
            Object.Instantiate(effect, vector2, Quaternion.identity);
            GameObject go = Object.Instantiate(particle, vector22, Quaternion.identity);
            if (unit.transform.localScale.x == 1)
            {
                go.GetComponent<ParticleSystem>().startSize = 0.4f;
                go.transform.localScale = new Vector3(-1, 1,1);
            }
            else
            {
                go.transform.localScale = new Vector3(1, 1,1);
                go.GetComponent<ParticleSystem>().startSize = 1f;
            }
            if (type == DamageType.Default)
            {
                unit.GetComponent<Animator>().Play(Name + "TakeDamage");
            }
            else if (type == DamageType.Hard )
            {
                unit.GetComponent<Animator>().Play(Name + "PushV1");
                Vector3 direction = unit.transform.position - _target.transform.position;
                float force = 3.5f;
                unit.GetComponent<Rigidbody2D>().AddForce(direction.normalized * force, ForceMode2D.Impulse);
            }
            else if (type == DamageType.SuperHard)
            {
                unit.GetComponent<Animator>().Play(Name + "PushV2");
                Vector3 direction = unit.transform.position - _target.transform.position;
                float force = 5f;
                unit.GetComponent<Rigidbody2D>().AddForce(direction.normalized * force, ForceMode2D.Impulse);
            }
            base.TakeDamage(damage, type);
        }

        protected override void Dead()
        {
            base.Dead();
            dead = true;
            unit.GetComponent<GunHandler>().KickOut();
            unit.GetComponent<BoxCollider2D>().enabled = false;
            unit.GetComponent<Animator>().SetBool("Attack", false);
            unit.GetComponent<Animator>().SetBool("AttackNoGun", false);
            unit.GetComponent<Animator>().Play(Name + "Dead");
            unit.transform.Find("Fade").gameObject.SetActive(false);
        }

        private bool _haveGun;
        private void InitWeapon(bool enable)
        {
            _haveGun = enable;
        }
        
        
        private void ContinueMove()
        {
            _canMove = true;
        }

        public override void UnitFixedUpdate()
        {
            
        }
        
        private void SetDefault()
        {
            SetHp(5);
            _gunHandler.Init();
            unit.gameObject.SetActive(false);
            unit.transform.rotation = Quaternion.identity;
            unit.GetComponent<BoxCollider2D>().enabled = true;
            dead = false;
        }
    }
}
