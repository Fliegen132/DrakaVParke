using System;
using DrakraVParke.Units;
using UnityEngine;

public class HeartCarrier : MonoBehaviour
{
   private bool vasCreate = false;
   private GameObject _heart;
   private void Update()
   {
      if(_heart == null)
         return;
      if (gameObject.GetComponent<Unit>().GetBehaviour().GetHP() <= 0 && !vasCreate)
      {
         _heart.transform.position = transform.position;
         _heart.GetComponent<HeartBehaviour>().Drop();
         vasCreate = true;
      }
   }

   public void SetHeart(GameObject heart)
   {
      _heart = heart;
   }

 
}
