using UnityEngine;

namespace _2048Figure.Architecture.ServiceLocator
{
    public class GAMEPLAY_SERVICELOCATOR : MonoBehaviour
    {
        [SerializeField] private ViewHP _viewHP;


        private void Awake()
        {
            ServiceLocator _service = new ServiceLocator();
            ServiceLocator.current.Register(_viewHP);
        }
    }
}