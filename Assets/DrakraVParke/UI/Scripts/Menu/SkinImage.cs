using DrakraVParke.Architecture.Menu;
using UnityEngine;
using UnityEngine.UI;

public class SkinImage : MonoBehaviour
{
    [SerializeField] private Image image;
    private void OnEnable()
    {
        image.sprite = Resources.Load<Sprite>("Skins/"+ DataMenu.BuyedHeroSkin);
    }
}
