using System;
using Core.Services.Interfaces;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       if(other.transform.TryGetComponent(out IDestructible destructible))
       {
           destructible.Destruct();
       }
    }
}
