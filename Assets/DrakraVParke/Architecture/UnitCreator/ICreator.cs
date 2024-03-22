using UnityEngine;

namespace DrakraVParke.Architecture
{
    public abstract class ICreator
    {
        public abstract GameObject Create(Transform parent, string skinName);
    }
}


