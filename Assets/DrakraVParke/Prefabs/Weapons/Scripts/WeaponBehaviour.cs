using System;
using System.Collections;
using DrakraVParke.Units;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public bool use;
    private Animator _animator;
    public bool CanDamage = false;
    public bool throwGun;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        use = true;
    }


    public void SetState()
    {
        if (!use)
        {
            transform.SetParent(null);
            transform.localPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 3);
            _animator.SetBool("Rotate", true);
            transform.GetChild(0).gameObject.SetActive(true);
            gameObject.GetComponent<Rigidbody2D>().simulated = true;
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 8, ForceMode2D.Impulse);
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 50;
            EnableGun();
        }
        else
        {
            _animator.SetBool("Rotate", false);
            transform.GetChild(0).gameObject.SetActive(false);
        }
        
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
        _animator.SetBool("Rotate", false);
        transform.localPosition = new Vector3(0,0, -3);
        transform.localRotation = Quaternion.Euler(0,0,0);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void EnableGun()
    {
        StartCoroutine(EnableGO());
    }

    private IEnumerator EnableGO()
    {
        yield return new WaitForSeconds(4f);
        if (transform.parent == null)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!CanDamage)
            return;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Unit>().GetBehaviour().TakeDamage(1, DamageType.Default);
            Destroy(gameObject);
        }
    }

    public void Throw(Vector2 throwDirection)
    {
        gameObject.transform.SetParent(null);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.3f, gameObject.transform.position.z -3);
        CanDamage = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().simulated = true; 
        GetComponent<Rigidbody2D>().gravityScale = 0; 
        GetComponent<Rigidbody2D>().velocity = throwDirection * 10; 
        EnableGun();
        throwGun = true;
        _animator.Play("ThrowRotate");
    }
}
