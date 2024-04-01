using _2048Figure.Architecture.ServiceLocator;
using TMPro;
using UnityEngine;

public class ViewScore : MonoBehaviour, IService
{
    public TextMeshProUGUI _scoreText;

    public void UpdateScore(int score)
    {
        _scoreText.text = $"{score}";
    }
    
}
