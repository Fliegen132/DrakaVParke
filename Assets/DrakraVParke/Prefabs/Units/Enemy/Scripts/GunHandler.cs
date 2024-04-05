using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunHandler : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private string gunName;
    private Animator _anim;
    public bool _haveGun;

    public Action<bool> SetAnim;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void Init()
    {
        int a = Random.Range(0, 3);
        if (a == 0)
        {
            _haveGun = false;
            SetAnim(_haveGun);
        }
        else
        {
            _haveGun = true;
            SetAnim(_haveGun);
            if (gunPoint.transform.childCount <= 0)
            {
                var res = Resources.Load<GameObject>(gunName);
                gun = Instantiate(res, gunPoint);
                
            } 
            
        }
    }

    public void KickOut()
    {
        if(!_haveGun)
            return;
        gun.gameObject.GetComponent<WeaponBehaviour>().use = false;
        gun.gameObject.GetComponent<WeaponBehaviour>().SetState();
        _haveGun = false;
        SetAnim(_haveGun);
    }
}
