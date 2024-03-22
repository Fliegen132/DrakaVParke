using _2048Figure.Architecture.ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

public class ViewHP : MonoBehaviour, IService
{
    [SerializeField] private Image _image;

    public void UpdateFill(float hp)
    {
        _image.fillAmount = hp;
    }
}
