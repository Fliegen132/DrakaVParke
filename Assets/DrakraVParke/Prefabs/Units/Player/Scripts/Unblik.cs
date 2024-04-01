using System.Collections;
using UnityEngine;

namespace DrakraVParke.Player
{
    public class Unblik : MonoBehaviour
    {
        public static Material _material;
        public static Transform[] _children;
        public static Unblik current = null;
        public void Start()
        {
            if (current == null)
            {
                current = this;
            }
        }

        public void StartUn()
        {
            StartCoroutine(Unblinck());
        }

        private IEnumerator Unblinck()
        {
            yield return new WaitForSeconds(.2f);
            foreach (Transform child in _children)
            {
                if (child.GetComponent<SpriteRenderer>() != null)
                {
                    child.GetComponent<SpriteRenderer>().material = _material;
                }
            }
        }

    }
}