using System;
using System.Threading;
using System.Threading.Tasks;
using _2048Figure.Architecture.ServiceLocator;
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
        private Material defaultMaterial;
        private Material blinkMaterial;
        private Transform[] _children;
        private EndGame _end;
        public Player(string name, GameObject player, IInput input, int handKickDamage = 1)
        {
            Name = name;
            _input = input;
            unit = player;
            //init damages
            _handKickDamage = handKickDamage;
            _viewModel = new ViewModel(this);
            ServiceLocator.current.Register(_viewModel);
            //----------
            defaultMaterial = unit.transform.GetChild(0).GetComponent<SpriteRenderer>().material;
            blinkMaterial = Resources.Load("PlayerBlink", typeof( Material)) as Material;
            _children = unit.GetComponentsInChildren<Transform>();
            Unblik._material = defaultMaterial;
            Unblik._children = _children;
            _end = ServiceLocator.current.Get<EndGame>();

        }

        public override void UnitUpdate()
        {
            if (dead)
            {
                unit.gameObject.transform.position = Vector2.MoveTowards(unit.gameObject.transform.position,
                    new Vector2(unit.gameObject.transform.position.x, -1.1772f), Time.deltaTime * 6);
                if(_end != null)
                    _end.End();
                return;
            }
            InputsUpdate();
        }
        
        private void InputsUpdate()
        {
            if(dead)
                return;
            int x = _input.DefaultKick();

            _input.Jump();
            _input.SitDown();
            unit.transform.localScale = new Vector2(x,unit.transform.localScale.y);
        }
        
        protected override void Dead()
        {
            dead = true;
            unit.GetComponent<Animator>().Play(Name + "Dead");
        }

        public override void TakeDamage(int damage, DamageType type)
        {
            if(dead)
                return;
            base.TakeDamage(damage, type);
            _viewModel.UpdateHP();
            Camera.main.gameObject.GetComponent<Animator>().Play("Shake");
            Blink();
        }

        private void Blink()
        {
            foreach (Transform child in _children)
            {
                if (child.GetComponent<SpriteRenderer>() != null)
                {
                    child.GetComponent<SpriteRenderer>().material = blinkMaterial;
                }
            }
            Unblik.current.StartUn();
        }
       
        public override void UnitFixedUpdate()
        {
            
        }
    }
}