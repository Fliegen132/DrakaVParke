using System.Collections.Generic;
using System.Linq;
using DrakraVParke.Units;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class SkinRandomize : MonoBehaviour
{
    private void Start()
    {
        Init();
    }
    
    public void Init()
    {
        for (int i = 1; i < 4; i++)
        {
            transform.Find("Head" + i).gameObject.SetActive(false);
            transform.Find("Body" + i).gameObject.SetActive(false);
        }
        SetRandomSprite();
    }
    
    private void SetRandomSprite()
    {
        int head = Random.Range(1, 4);
        int body = Random.Range(1, 4);

        transform.Find("Head" + head).gameObject.SetActive(true);
        transform.Find("Body" + body).gameObject.SetActive(true);

    }
}