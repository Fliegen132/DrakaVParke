using DrakraVParke.Units;
using UnityEngine;
using Unit = DrakraVParke.Units.Unit;

namespace DrakaVParke.Player
{
    public class DesktopInput : IInput
    {
        private AnimationController _animatorController;
        private Unit _player;
        
        private bool _jump = false;
        private bool _sitDown = false;
        
        private int _direct = 1;
        private int kickCount = 0;
        #region KickAnimations
        //для всех одни
        private readonly string[] KICK_ANIM = { "HandKick",  "LegKick"};
        private readonly string[] TURN_KICK_ANIM = { "TurnHandKick", "TurnLegKick"};
        
        private readonly string JUMP_KICK_ANIM = "JumpKick";
        
        private readonly string[] SIT_KICK_ANIM = {"SitKick", "SitHandKick"};
        private readonly string UPPER_CUT_ANIM = "UpperCut";
        private readonly string GUN_THROW_ANIM = "ThrowGun";
        #endregion
        //for jump
        private readonly float _minY = -1.15f;
        private float _maxY;

        private Animator _anim;

        private GameObject _gun;
        private bool _goBack = false;
        private bool _downAttack;

        public DesktopInput(AnimationController anim, Unit player)
        {
            _animatorController = anim;
            _player = player;
            _anim = anim.GetAnimator();
            _player.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        
        public int DefaultKick()
        {
            
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if(_gun != null)
                    ThrowGun();
                _direct = 1;
                if(_gun == null) 
                    Attack();
                
                kickCount++;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if(_gun != null)
                    ThrowGun();
                _direct = -1;
                if(_gun == null) 
                    Attack();
               
                kickCount++;
            }
            if (_downAttack)
            {
                DownAttack();
            }
            return _direct;
        }
        //для всех один
        private void Attack()
        {
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
                    kickCount = 0;
                }
                else
                {
                    
                    PlayAnim(KICK_ANIM);
                }
            }

            if (!TryGetWeapon())
                HitDetected(damageType);
        }

        private void ThrowGun()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && _direct != -1)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && _direct != 1)
            {
                return;
            }
            PlayAnim(GUN_THROW_ANIM);
            _gun.gameObject.transform.SetParent(null);
            _gun.gameObject.transform.position = new Vector3(_gun.gameObject.transform.position.x, _gun.gameObject.transform.position.y + 0.3f, _gun.gameObject.transform.position.z -3);
            _gun.GetComponent<WeaponBehaviour>().CanDamage = true;
            _gun.GetComponent<BoxCollider2D>().enabled = true;
            Vector2 throwDirection = new Vector2(_direct, 0);
            _gun.GetComponent<Rigidbody2D>().simulated = true; 
            _gun.GetComponent<Rigidbody2D>().gravityScale = 0; 
            _gun.GetComponent<Rigidbody2D>().velocity = throwDirection * 10; 
            _gun = null;
        }

        //для всех один
        private int PlayAnim(string[] anim)
        {
            int currentAnim = Random.Range(0, anim.Length);
            _animatorController.SetAnimation(_player.GetBehaviour().Name + anim[currentAnim]);
            return currentAnim;
        }
        private void PlayAnim(string anim)
        {
            _animatorController.SetAnimation(_player.GetBehaviour().Name + anim);
        }

        //для всех один
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
            Transform gunPoint = FindDeepChild(_player.transform, "GunPoint");

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

        private bool _damageDealt = false;

        private void DownAttack()
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
                        _damageDealt = true; // Устанавливаем флаг, что урон был нанесен
                    }
                }
            }
        }

        //для всех один
        private void HitDetected(DamageType type)
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(new Vector2(_player.transform.position.x, _player.transform.position.y + 0.5f), new Vector2(_direct, 0), 1.5f);
            if (hit != null)
            {
                for(int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].collider.GetComponent<Unit>())
                    {
                        if (hit[i].collider.GetComponent<Unit>().CompareTag("Enemy"))
                        {
                            
                            hit[i].collider.GetComponent<Unit>().GetBehaviour().TakeDamage(1, type);
                        }
                    }
                }
            }
        }

        private float _downSpeed = 6;
        public void Jump()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && !_sitDown && !_jump)
            {
                _jump = true;
                _anim.SetBool("SitDown", false);
                _anim.SetBool("Jump", true);
            }
    
            if (_jump)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    _downAttack = true;
                    _goBack = true;
                    _downSpeed = 8;
                    PlayAnim("JumpDownKick");
                }

                if(_player.transform.position.y >= 1.5f)
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

                if(_player.transform.position.y <= 1.5f && !_goBack)
                {
                    _player.gameObject.transform.position = Vector2.MoveTowards(_player.gameObject.transform.position, 
                        new Vector2(_player.gameObject.transform.position.x, 1.5f), Time.deltaTime * 8);
                }
                if (_player.transform.position.y <= _minY)
                {
                    _jump = false;
                    _anim.SetBool("JumpDown", false);
                    _anim.SetBool("JumpDownAttack", false);
                    _downSpeed = 6;
                    _goBack = false;
                    _downAttack = false;
                    _damageDealt = false;
                }
            }
        }
        
        public void SitDown()
        {
            if (_sitDown && Input.GetKeyDown(KeyCode.UpArrow))
            {
                _anim.SetBool("SitDown", false);
                _sitDown = false;
                PlayAnim(UPPER_CUT_ANIM);
                HitDetected(DamageType.SuperHard);
            }
            
            if (Input.GetKeyDown(KeyCode.DownArrow) && !_jump)
            {
                _anim.SetBool("SitDown", true);
                _sitDown = true;
            }
        }
    }
}