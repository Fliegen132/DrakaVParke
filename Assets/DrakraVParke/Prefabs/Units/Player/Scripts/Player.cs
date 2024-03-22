using DrakaVParke.Player;
using DrakraVParke.Units;
using UnityEngine;

namespace DrakraVParke.Player
{
    public class Player : UnitBehaviour
    {
        private IInput _input;
        #region damages

        private int _handKickDamage;
            
        #endregion

        private ViewModel _viewModel;
        public Player(string name, GameObject player, IInput input, int handKickDamage = 1)
        {
            Name = name;
            _input = input;
            unit = player;
            //init damages
            _handKickDamage = handKickDamage;
            _viewModel = new ViewModel(this);
            //----------
        }

        

        public override void UnitUpdate()
        {
            if(dead)
                return;
            InputsUpdate();
            
        }
        
        private void InputsUpdate()
        {
            if(dead)
                return;
            int x = _input.DefaultKick();
            unit.transform.localScale = new Vector2(x,unit.transform.localScale.y);
            
            _input.Jump();
            _input.SitDown();
        }
        
        protected override void Dead()
        {
            base.Dead();
            unit.GetComponent<Animator>().SetBool("Dead", true);
        }

        public override void TakeDamage(int damage, DamageType type)
        {
            base.TakeDamage(damage, type);
            _viewModel.UpdateHP();
        }

        public override void UnitFixedUpdate()
        {
        }
    }
}