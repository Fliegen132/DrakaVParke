using UnityEngine;

namespace DrakraVParke.Units
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Unit : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private UnitBehaviour _unit;
        
        public void Init(UnitBehaviour unit)
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

            
            _unit = unit;
        }

        private void Update()
        {
            _unit.UnitUpdate();
        }

        private void FixedUpdate()
        {
            _unit.UnitFixedUpdate();
        }

        
        public UnitBehaviour GetBehaviour() => _unit;
    }
}
