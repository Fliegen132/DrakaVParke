using System;
using DrakraVParke.Architecture.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class CharacterChooser : MonoBehaviour
{
    [SerializeField] private Transform currentChooseSkin;
    
    [SerializeField] private Transform availableSkins;

    [SerializeField] private int currentSkin = 0;
    
    private string _characterName;
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private GameObject[] booms;
    [SerializeField] private SkinSelector _skinSelector;
    private void Awake()
    {
        DataMenu.HeroName = "Babushka";
    }

    private void Update()
    {
        Inputs();
    }

    private void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeNext();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeBack();
        }
    }
    
    public void ChangeNext()
    {
        ChangeSprites(false);

        if (currentSkin >= availableSkins.childCount - 1)
        {
            currentSkin = 0;
        }
        else
        {
            currentSkin++;
        }

       
        ChangeSprites(true); 
    }

    public void ChangeBack()
    {
        ChangeSprites(false); 

        if (currentSkin <= 0)
        {
            currentSkin = availableSkins.childCount - 1;
        }
        else
        {
            currentSkin--;
        }

        ChangeSprites(true); 
    }

    private void ChangeSprites(bool setActive)
    {
        for (int i = 0; i < booms.Length; i++)
        {
            booms[i].SetActive(true);
        }
        availableSkins.GetChild(currentSkin).gameObject.SetActive(setActive);
        currentChooseSkin.GetChild(currentSkin).gameObject.SetActive(setActive);
        _characterName = currentChooseSkin.GetChild(currentSkin).gameObject.name;
        characterNameText.text = _characterName;
        DataMenu.HeroName = _characterName;
        _skinSelector.Init();
    }
}
