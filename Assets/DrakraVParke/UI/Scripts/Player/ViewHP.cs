using _2048Figure.Architecture.ServiceLocator;
using DrakaVParke.Architecture;
using DrakraVParke.Units;
using UnityEngine;
using UnityEngine.UI;

public class ViewHP : MonoBehaviour, IService
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _characterIcon;

    [SerializeField] private Image[] _characterIconsLoad;

    private void Start()
    {
        Invoke(nameof(Init), 0.1f);
    }

    private void Init()
    {
        if (UnitList.Player.name.Contains("Babushka"))
        {
            var icons = Resources.LoadAll<Sprite>("BabushkaIcon"); 
            _characterIconsLoad = new Image[icons.Length];
            for (int i = 0; i < icons.Length; i++)
            {
                GameObject newImageObject = new GameObject("BabushkaIcon" + i); 
                _characterIconsLoad[i] = newImageObject.AddComponent<Image>();
                _characterIconsLoad[i].sprite = icons[i];
            }
            _characterIcon.sprite = _characterIconsLoad[0].sprite;
        }
        else
        {
            var icons = Resources.LoadAll<Sprite>("CatIcon"); // Загружаем все спрайты из папки CatIcon
            _characterIconsLoad = new Image[icons.Length];
            for (int i = 0; i < icons.Length; i++)
            {
                GameObject newImageObject = new GameObject("CatIcon" + i);
                _characterIconsLoad[i] = newImageObject.AddComponent<Image>();
                _characterIconsLoad[i].sprite = icons[i]; // Присваиваем каждому элементу массива спрайт из ресурсов
            }
            _characterIcon.sprite = _characterIconsLoad[0].sprite;
        }
    }

    public void UpdateFill(float hp)
    {
        _image.fillAmount = hp;
        if (hp * 10 > 6)
        {
            _characterIcon.sprite = _characterIconsLoad[0].sprite;
        }
        if (hp * 10 <= 6)
        {
            _characterIcon.sprite = _characterIconsLoad[1].sprite;
        }
        if (hp * 10 <= 3)
        {
            _characterIcon.sprite = _characterIconsLoad[2].sprite;
        }
    }
}
