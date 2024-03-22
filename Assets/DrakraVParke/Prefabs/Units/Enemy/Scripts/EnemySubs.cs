using System;
using UnityEngine;

public class EnemySubs : MonoBehaviour
{
    public Action _HitDetected;
    public Action _DeadAction;
    public Action _CanMove;
    public void PlayAction()
    {
        _HitDetected?.Invoke();
    }

    public void DeadAction()
    {
        _DeadAction?.Invoke();
    }

    public void CanMove()
    {
        _CanMove?.Invoke();
    }
}
