using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBehavour : MonoBehaviour
{
    private void Update()
    {
        transform.position = new Vector3(transform.parent.transform.position.x, -1.02f, transform.parent.transform.position.z);
    }
}
