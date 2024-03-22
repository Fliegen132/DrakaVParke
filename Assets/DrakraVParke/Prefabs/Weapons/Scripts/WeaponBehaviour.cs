using System;
using DrakraVParke.Units;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public bool use;
    private Animator _animator;
    public bool CanDamage = false;
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
}
