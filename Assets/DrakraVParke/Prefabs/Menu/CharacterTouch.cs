using UnityEngine;
using UnityEngine.Events;

public class CharacterTouch : MonoBehaviour
{
   [SerializeField] private UnityEvent click;
   private void OnMouseDown()
   {
      click.Invoke();;
   }
}
