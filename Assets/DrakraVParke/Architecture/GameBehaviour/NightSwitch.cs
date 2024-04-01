using DrakraVParke.Architecture;
using DrakraVParke.Architecture.Menu;
using UnityEngine;
using UnityEngine.UI;

public class NightSwitch : MonoBehaviour
{
    [SerializeField] private GameObject light;
    [SerializeField] private GameObject icon;

    [SerializeField] private Color nightColor;
    [SerializeField] private Color dayColor;

    [SerializeField] private GameObject nightIcon;
    [SerializeField] private GameObject dayIcon;
    public void Start()
    {
        if (DataMenu.Night)
        {
            light.SetActive(true);
        }
    }
    
    public void Switch()
    {
        DataMenu.Night = !DataMenu.Night;
        if (DataMenu.Night)
        {
            icon.GetComponent<Image>().color = dayColor;
            light.SetActive(true);
            dayIcon.SetActive(true);
            nightIcon.SetActive(false);
        }
        else
        {
            icon.GetComponent<Image>().color = nightColor;
            light.SetActive(false);
            nightIcon.SetActive(true);
            dayIcon.SetActive(false);
        }
    }
}
