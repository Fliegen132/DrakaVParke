using DrakraVParke.Architecture.Menu;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelector : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private RectTransform content;
    [SerializeField] private RectTransform babushkaContent;
    [SerializeField] private RectTransform catContent;
    [SerializeField] private RectTransform listItem;
    [SerializeField] private HorizontalLayoutGroup hor;
    private float speed;
    private float snapForce = 100;
    [SerializeField] private GameObject[] skins;

    public GameObject[] babushkaSkins;
    public GameObject[] catSkins;
    private int maxIndex;

    [SerializeField] private ViewBuySkin _viewBuySkin;

    private string _skinName;
    private static int i;

    private void Start()
    {
        Load();
        Init();
        _skinName = "Babushka";
        DataMenu.AvailableSkins.Add("Babushka");
        DataMenu.AvailableSkins.Add("cat");
        for(int i = 0; i < DataMenu.AvailableSkins.Count; i++)
            Debug.Log(DataMenu.AvailableSkins[i]);
        UpdateSkins();
    }

    private void Load()
    {
        ILoad();
        SkinLoad();
    }
    private void ILoad()
    {
        i = PlayerPrefs.GetInt("skinCount");
    }

    private void SkinLoad()
    {
        for (int j = 0; j < i; j++)
        {
            string a = PlayerPrefs.GetString($"skin{j}");
            DataMenu.AvailableSkins.Add(a);
            Debug.Log(a);
        }
    }
    
    public void UpdateSkins()
    {
        for (int i = 1; i < babushkaSkins.Length; i++)
        {
            if (DataMenu.AvailableSkins.Contains(babushkaSkins[i].name))
            {
                babushkaSkins[i].transform.GetChild(0).gameObject.SetActive(false);
                babushkaSkins[i].GetComponent<Image>().color = Color.white;
            }
        }
        
        for (int i = 1; i < catSkins.Length; i++)
        {
            if (DataMenu.AvailableSkins.Contains(catSkins[i].name))
            {
                catSkins[i].transform.GetChild(0).gameObject.SetActive(false);
                catSkins[i].GetComponent<Image>().color = Color.white;
            }
        }
        
    }
    public void Init()
    {
        if(content != null)
            content.gameObject.SetActive(false);
        if (DataMenu.HeroName == "Babushka")
        {
            content = babushkaContent;
            skins = babushkaSkins;
        }
        if (DataMenu.HeroName == "Cat")
        {
            skins = catSkins;
            content = catContent;
        }

        _scrollRect.content = content;
        content.gameObject.SetActive(true);
        maxIndex = content.childCount - 1;

    }
    
    private void Update()
    {
        if(_viewBuySkin.WindowOpen)
            return;
        int currentItem = Mathf.RoundToInt((0 - content.localPosition.x / (listItem.rect.width + hor.spacing)));
        Debug.Log(currentItem);
        currentItem = Mathf.Clamp(currentItem, -2, maxIndex-2);

        if (_scrollRect.velocity.magnitude < 200)
        {
            speed += snapForce * Time.deltaTime;
            float targetX = 0 - (currentItem * (listItem.rect.width + hor.spacing));
            content.localPosition = new Vector3(Mathf.MoveTowards(content.localPosition.x, targetX, speed), content.localPosition.y, content.localPosition.z);

            Debug.Log(skins[currentItem+2].name);
            DataMenu.HeroSkin = skins[currentItem + 2].name;
            _skinName = skins[currentItem + 2].name;
            
        }
    }

    public string GetSkinName() => _skinName;

    public void ClickForBuy(string Name)
    {
        _viewBuySkin.SetNameSelector(this);
        DataMenu.BuyedHeroSkin = Name;
        _viewBuySkin.OpenShop();
        
    }
}