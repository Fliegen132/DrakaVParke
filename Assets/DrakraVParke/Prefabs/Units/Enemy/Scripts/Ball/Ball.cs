using System;
using System.Collections;
using DrakraVParke.Units;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    private readonly float _maxHeight = 9;
    private readonly float _minHeight = 7;
    private bool rightLocomotion;
    
    public void Init()
    {
        gameObject.SetActive(true);
        int height = Random.Range(0, 2);
        if(height == 0)
            transform.localPosition  = new Vector3(0, _minHeight,0); 
        else
            transform.localPosition  = new Vector3(0, _maxHeight, 0); 

        transform.localRotation = Quaternion.Euler(0, 0, 100);
        
        int direction = Random.Range(0, 2);

        if (direction == 0)
        {
            rightLocomotion = false;

            transform.localRotation = Quaternion.Euler(0, 0, 100); 

        }
        else
        {
            rightLocomotion = true;
            transform.localRotation = Quaternion.Euler(0, 0, -100); 
        }
    }

    private void Update()
    {
        Locomotion();
        StartCoroutine(EnableGO());
    }
    
    private void Locomotion()
    {
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + (rightLocomotion ? 1 : -1) * Time.deltaTime * 65f); 
    }

    private IEnumerator EnableGO()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Unit>().GetBehaviour().TakeDamage(1, DamageType.Default);
                        
        }
    }
}