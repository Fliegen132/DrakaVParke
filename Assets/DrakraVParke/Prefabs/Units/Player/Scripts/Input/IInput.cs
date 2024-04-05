using _2048Figure.Architecture.ServiceLocator;
using DrakraVParke.Player;
using DrakraVParke.Units;
using UnityEngine;

namespace DrakaVParke.Player
{
    public abstract class IInput
    {
        protected Unit _player;
        protected GameObject _gun;

        protected AnimationController _animatorController;
        protected Animator _anim;
        protected int _direct = 1;
        protected bool _jump = false;
        protected bool _sitDown = false;
        
        protected int kickCount = 0;
        #region KickAnimations
        //для всех одни
        protected readonly string[] KICK_ANIM = { "HandKick",  "LegKick"};
        protected readonly string[] TURN_KICK_ANIM = { "TurnHandKick", "TurnLegKick"};
        
        protected readonly string JUMP_KICK_ANIM = "JumpKick";
        
        protected readonly string[] SIT_KICK_ANIM = {"SitKick", "SitHandKick"};
        protected readonly string UPPER_CUT_ANIM = "UpperCut";
        protected readonly string GUN_THROW_ANIM = "ThrowGun";
        #endregion
        protected readonly float _minY = -1.15f;

        protected bool _damageDealt = false;

        protected bool _goBack = false;
        protected bool _downAttack;

        #region sizeCollider
        protected readonly float _colliderDefSize = 2.375983f;
        protected readonly float _colliderDefOffset = 1.242492f;
        
        protected readonly float _colliderSitSize = 1.082148f;
        protected readonly float _colliderSitOffset = 0.595575f;

        #endregion
        
        protected float _downSpeed = 4;
        private float _upSpeed = 10;

        private Transform gunPoint;

        /// <summary>
        /// Для урона
        /// </summary>
        private int _baseDamage = 1;
        private int _hardDamage = 1;
        
        public IInput(Unit player , AnimationController anim)
        {
            _player = player;
            _animatorController = anim;
            _anim = anim.GetAnimator();
            _player.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            gunPoint = FindDeepChild(_player.transform, "GunPoint");
        }
        private Transform FindDeepChild(Transform parent, string name)
        {
            Transform result = parent.Find(name);
            if (result != null)
                return result;

            foreach (Transform child in parent)
            {
                result = FindDeepChild(child, name);
                if (result != null)
                    return result;
            }

            return null;
        }
        
        private bool TryGetWeapon()
        {
            if (gunPoint != null)
            {
                Debug.Log(gunPoint.name);
                Collider2D hit = Physics2D.OverlapCircle(new Vector2(gunPoint.position.x + _direct / 2, gunPoint.position.y), 1.5f, LayerMask.GetMask("Weapon"));

                if (hit != null)
                {
                    if (hit.GetComponent<WeaponBehaviour>() && !hit.GetComponent<WeaponBehaviour>().CanDamage)
                    {
                        Debug.Log(hit.name);
                        hit.GetComponent<WeaponBehaviour>().SetParent(gunPoint);
                        _gun = hit.GetComponent<WeaponBehaviour>().gameObject;
                        _gun.GetComponent<SpriteRenderer>().sortingOrder = 11;
                        return true;
                    }
                }
            }
            return false;
        }
        
        private bool TryGetHeart()
        {
            Debug.Log("HEEEEEEEEEEEEARTTRRRYYY");
            if (gunPoint != null)
            {
                Collider2D hit = Physics2D.OverlapCircle(new Vector2(gunPoint.position.x + _direct / 2, gunPoint.position.y), 1.5f, LayerMask.GetMask("Weapon"));

                if (hit != null)
                {
                    if (hit.GetComponent<HeartBehaviour>())
                    {
                        Debug.Log(hit.name);
                        hit.GetComponent<HeartBehaviour>().Take();
                    }
                }
            }
            return false;
        }
        protected void Attack()
        {
            int damage = _baseDamage;
            DamageType damageType = DamageType.Default;
            if (_jump)
            {
                PlayAnim(JUMP_KICK_ANIM);
                
            }
            else if (_sitDown)
            {
                PlayAnim(SIT_KICK_ANIM);
            }
            else if (_jump == false)
            {
               
                if (kickCount >= 5)
                {
                    int damageTypeValue = PlayAnim(TURN_KICK_ANIM);
                    damageType = (DamageType)(damageTypeValue + 1);
                    damage = _hardDamage;
                    kickCount = 0;
                }
                else
                {
                    
                    PlayAnim(KICK_ANIM);
                }
            }

            if (!TryGetWeapon())
                if(!TryGetHeart())
                    HitDetected(damageType, damage);
        }

        protected int PlayAnim(string[] anim)
        {
            int currentAnim = Random.Range(0, anim.Length);
            _animatorController.SetAnimation(_player.GetBehaviour().Name + anim[currentAnim]);
            return currentAnim;
        }
        protected void PlayAnim(string anim)
        {
            _animatorController.SetAnimation(_player.GetBehaviour().Name + anim);
        }

        private void HitDetected(DamageType type, int damage)
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(
                new Vector2(_player.transform.position.x, _player.transform.position.y + 0.5f), new Vector2(_direct, 0),
                1.5f);
            if (hit != null)
            {
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].collider.GetComponent<Unit>())
                    {
                        if (hit[i].collider.GetComponent<Unit>().CompareTag("Enemy"))
                        {
                            hit[i].collider.GetComponent<Unit>().GetBehaviour().TakeDamage(damage, type);
                            int a = Random.Range(1, 3);
                            if(type == DamageType.SuperHard)
                                PlayAudio("EXTRAKICK");
                            else
                            {
                                PlayAudio($"KICK{a}");
                            }
                        }
                    }
                }
            }
        }
        
        protected void DownAttack()
        {
            if (!_damageDealt)
            {
                Collider2D hit = Physics2D.OverlapCircle(new Vector2(_player.transform.position.x + _direct / 2, _player.transform.position.y), 1.5f, LayerMask.GetMask("Enemy"));

                if (hit != null)
                {
                    if (hit.GetComponent<Unit>())
                    {
                        hit.GetComponent<Unit>().GetBehaviour().TakeDamage(1, DamageType.Hard);
                        Debug.Log(hit.gameObject.name);
                        _damageDealt = true; 
                        PlayAudio("EXTRAKICK");
                    }
                }
            }
        }
        
        protected void ThrowGun()
        {
            PlayAnim(GUN_THROW_ANIM);
            Vector2 throwDirection = new Vector2(_direct, 0);
            _gun.GetComponent<WeaponBehaviour>().Throw(throwDirection);
            PlayAudio("throwingKnife");
            AchievementsManager.DropKnife();
            _gun = null;
        }

        protected void InJump()
        {
            if (_player.transform.position.y >= .7f && !_goBack)
            {
                _upSpeed = 10;
            }

            if (_player.transform.position.y <= 1f && _goBack)
            {
                _downSpeed = 6;
            }

            if (_player.transform.position.y >= 1.4f)
            {
                _goBack = true;
            }

            if (_goBack)
            {
                _player.gameObject.transform.position = Vector2.MoveTowards(_player.gameObject.transform.position,
                    new Vector2(_player.gameObject.transform.position.x, -1.1772f), Time.deltaTime * _downSpeed);
                _anim.SetBool("Jump", false);
                _anim.SetBool("JumpDown", true);
            }

            if (_player.transform.position.y <= 1.5f && !_goBack)
            {
                _player.gameObject.transform.position = Vector2.MoveTowards(_player.gameObject.transform.position,
                    new Vector2(_player.gameObject.transform.position.x, 1.5f), Time.deltaTime * _upSpeed);
            }

            if (_player.transform.position.y <= _minY && _goBack)
            {
                _jump = false;
                _anim.SetBool("JumpDown", false);
                _anim.SetBool("JumpDownAttack", false);
                _upSpeed = 8;
                _downSpeed = 6;
                _goBack = false;
                _downAttack = false;
                _damageDealt = false;
            }
        }

        public virtual int DefaultKick()
        {

            return 1;
        }

        public virtual void Jump()
        {
        }

        public virtual void SitDown()
        {
        }

        protected void UpperCut()
        {
            _anim.SetBool("SitDown", false);
            _player.GetComponent<BoxCollider2D>().offset = new Vector2(0, _colliderDefOffset);
            _player.GetComponent<BoxCollider2D>().size = new Vector2(1, _colliderDefSize);
            _sitDown = false;
            PlayAnim(UPPER_CUT_ANIM);
            HitDetected(DamageType.SuperHard, _hardDamage);
        }

        private void PlayAudio(string name)
        {
            AudioClip clip = Resources.Load<AudioClip>(name);
            _player.GetComponent<AudioSource>().clip = clip;
            _player.GetComponent<AudioSource>().Play();;
        }
    }
}


