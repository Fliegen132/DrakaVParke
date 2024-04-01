using UnityEngine;

namespace _2048Figure.Architecture.ServiceLocator
{
    public class MAINMENU_SERVICELOCATOR : MonoBehaviour
    {
        [SerializeField] private ViewCoins _viewCoins;
        private void Awake()
        {
            ServiceLocator _service = new ServiceLocator();
            
            ServiceLocator.current.Register(_viewCoins);
            
        }
    }
}