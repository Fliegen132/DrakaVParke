using DrakraVParke.Player;
using DrakraVParke.Units;
using UnityEngine;
using Unit = DrakraVParke.Units.Unit;

namespace DrakaVParke.Player
{
    public class DesktopInput : IInput
    {
        public DesktopInput(Unit player, AnimationController anim) : base(player, anim)
        {
        }
        public override int DefaultKick()
        {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (_gun != null)
                        TryThrowGun();
                    _direct = 1;
                    if (_gun == null)
                        Attack();

                    kickCount++;
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (_gun != null)
                        TryThrowGun();
                    _direct = -1;
                    if (_gun == null)
                        Attack();

                    kickCount++;
                }

                if (_downAttack)
                {
                    DownAttack();
                }

                return _direct;
        }

        private void TryThrowGun()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && _direct != -1)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && _direct != 1)
            {
                return;
            }
            ThrowGun();
        }

        public override void Jump()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && !_sitDown && !_jump)
            {
                _jump = true;
                _anim.SetBool("SitDown", false);
                _anim.SetBool("Jump", true);
                AchievementsManager.CheckJump();
            }
            if (_jump)
            {
                InJump();
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    _downAttack = true;
                    _goBack = true;
                    _downSpeed = 8;
                    PlayAnim("JumpDownKick");
                }
            }
        }
        
        public override void SitDown()
        {
            if (_sitDown && Input.GetKeyDown(KeyCode.UpArrow))
            {
               base.UpperCut();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && !_jump)
            {
                _anim.SetBool("SitDown", true);
                AchievementsManager.CheckSitDown();
                _player.GetComponent<BoxCollider2D>().offset = new Vector2(0,_colliderSitOffset);
                _player.GetComponent<BoxCollider2D>().size = new Vector2(1, _colliderSitSize);
                _sitDown = true;
            }
        }

      
    }
}