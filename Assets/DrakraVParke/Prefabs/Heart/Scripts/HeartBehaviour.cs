using System.Collections;
using System.Reflection;
using _2048Figure.Architecture.ServiceLocator;
using DrakaVParke.Architecture;
using DrakraVParke.Units;
using UnityEngine;
using UnityEngine.UI;

public class HeartBehaviour : MonoBehaviour
{
    [SerializeField] private RectTransform _hpBar;
    private Vector2 targetPosition;
    private bool isMoving = false;
    public float arrivalThreshold = 10f;
    private bool hasReachedTarget = false;

    private void Awake()
    {
        gameObject.SetActive(false);
    }


    private void Update()
    {
        if (isMoving && !hasReachedTarget)
        {
            float step = Time.deltaTime * 10 ;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (Vector3.Distance(transform.position, targetPosition) < arrivalThreshold)
            {
                Done();
            }
        }
    }

    public void Drop()
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 8, ForceMode2D.Impulse);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 50;
        StartCoroutine(Off());
    }

    private IEnumerator Off()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    private void Done()
    {
        UnitList.Player.GetComponent<Unit>().GetBehaviour().AddHP(3);
        ServiceLocator.current.Get<ViewModel>().UpdateHP();
        hasReachedTarget = true; 
        gameObject.SetActive(false);
        isMoving = false;
    }

    public void Take()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        targetPosition = Camera.main.ScreenToWorldPoint(_hpBar.position);
        hasReachedTarget = false;
        isMoving = true;
    }
}
