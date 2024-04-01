using System.Collections.Generic;
using System.Linq;
using DrakraVParke.Units;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class SkinRandomize : MonoBehaviour
{
    [SerializeField] private SpriteLibraryAsset _spriteLibrary;

    [SerializeField] private GameObject _head;
    [SerializeField] private GameObject _body;
    private string name;
    private void Start()
    {
        Init();
        name = gameObject.GetComponent<Unit>().GetBehaviour().Name;
    }

    private void GetRandomSprite()
    {
        List<string> head = _spriteLibrary.GetCategoryLabelNames($"{name}Head").ToList();
        List<string> body = _spriteLibrary.GetCategoryLabelNames($"{name}Body").ToList();
        
        if (head.Count > 0)
        {
            int a = Random.Range(0, head.Count);
            int b = Random.Range(0, body.Count);
            _head.GetComponent<SpriteRenderer>().sprite =
                 _spriteLibrary.GetSprite($"{name}Head", head[a]);
            _body.GetComponent<SpriteRenderer>().sprite = _spriteLibrary.GetSprite($"{name}Body", body[b]);
        }
    }

    public void Init()
    { 
        GetRandomSprite();
    }
}