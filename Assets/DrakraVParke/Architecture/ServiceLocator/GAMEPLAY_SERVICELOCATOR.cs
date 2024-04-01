using DrakraVParke.Player;
using UnityEngine;

namespace _2048Figure.Architecture.ServiceLocator
{
    public class GAMEPLAY_SERVICELOCATOR : MonoBehaviour
    {
        [SerializeField] private ViewHP _viewHP;
        [SerializeField] private ViewScore _viewScore;
        [SerializeField] private Tutorial _tutorial;
        [SerializeField] private EndGame _endGame;
        [SerializeField] private AchievementsDescription _achievementsDescription;
        private ViewModelScore _viewModelScore;
    
        private void Awake()
        {
            ServiceLocator _service = new ServiceLocator();
            ServiceLocator.current.Register(_viewHP);
            ServiceLocator.current.Register(_viewScore);
            _viewModelScore = new ViewModelScore();
            ServiceLocator.current.Register(_viewModelScore);
            ServiceLocator.current.Register(_tutorial);
            ServiceLocator.current.Register(_endGame);
            ServiceLocator.current.Register(_achievementsDescription);
        }
    }
}