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

    [SerializeField] private GameObject[] babushkaSkins;
    [SerializeField] private GameObject[] catSkins;
    private int maxIndex;
    private void Start()
    {
        if (DataMenu.heroName == "Babushka")
        {
            content = babushkaContent;
            skins = babushkaSkins;
        }
        if (DataMenu.heroName == "Cat")
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
        int currentItem = Mathf.RoundToInt((0 - content.localPosition.x / (listItem.rect.width + hor.spacing)));
        Debug.Log(currentItem);
        currentItem = Mathf.Clamp(currentItem, -2, maxIndex-2);

        if (_scrollRect.velocity.magnitude < 200)
        {
            speed += snapForce * Time.deltaTime;
            float targetX = 0 - (currentItem * (listItem.rect.width + hor.spacing));
            content.localPosition = new Vector3(Mathf.MoveTowards(content.localPosition.x, targetX, speed), content.localPosition.y, content.localPosition.z);

            Debug.Log(skins[currentItem+2].name);
            DataMenu.heroSkin = skins[currentItem + 2].name;
        }
    }
}