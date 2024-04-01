using DrakraVParke.Player;
using DrakraVParke.Units;
using UnityEngine;

namespace DrakaVParke.Player
{
    public class MobileInput : IInput
    {
        private bool _clickStarted = false;
        private Vector2 _clickPosition;
        private Vector2 _releasePosition;
        private bool canKick;
        
        private float Y = 0;
        private float yDistance = 3f;
        public MobileInput(Unit player, AnimationController anim) : base(player, anim)
        {
        }

        public override int DefaultKick()
        {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && canKick)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.position.x >= Screen.width / 2)
                    {
                        if (_gun != null)
                            TryThrowGun();
                        _direct = 1;
                        if (_gun == null)
                            Attack();

                        kickCount++;
                    }
                    else
                    {
                        if (_gun != null)
                            TryThrowGun();
                        _direct = -1;
                        if (_gun == null)
                            Attack();

                        kickCount++;
                    }
                    base.DefaultKick();
                }

                if (_downAttack && canKick)
                {
                    DownAttack();
                }
                return _direct;
        }

        private void TryThrowGun()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && _direct != -1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.position.x <= Screen.width / 2)
                    return;
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && _direct != 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.position.x >= Screen.width / 2)
                    return;
            }

            ThrowGun();
        }

        public override void Jump()
        {
            CheckSwap();
            if (_jump)
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    if (Y < -yDistance)
                    {
                        _downAttack = true;
                        _goBack = true;
                        _downSpeed = 10;
                        PlayAnim("JumpDownKick");
                        canKick = true;
                        Y = 0;
                    }
                }
                InJump();
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended  && !_jump)
            {
                if (Y > yDistance && !_sitDown && !_jump)
                {
                    base.Jump();
                    _jump = true;
                    AchievementsManager.CheckJump();
                    _anim.SetBool("SitDown", false);
                    _anim.SetBool("Jump", true);
                    canKick = true;
                    Y = 0;
                }
            }
        }
        
        public override void SitDown()
        {
            CheckSwap();
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (Y > yDistance && _sitDown)
                {
                    UpperCut();
                    canKick = false;
                }
                else if (Y < -yDistance && Y < 0 && !_jump)
                {
                    _anim.SetBool("SitDown", true);
                    _player.GetComponent<BoxCollider2D>().offset = new Vector2(0, _colliderSitOffset);
                    _player.GetComponent<BoxCollider2D>().size = new Vector2(1, _colliderSitSize);
                    _sitDown = true;
                    AchievementsManager.CheckSitDown();
                    canKick = false;
                    base.SitDown();
                }
                
                Y = 0;
                canKick = true;
            }
        }

        private void CheckSwap()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Y = -Input.GetTouch(0).deltaPosition.y;
            }
            if (Y > yDistance || Y < -yDistance && Y != 0)
            {
                canKick = false;
            }
        }
    }
}