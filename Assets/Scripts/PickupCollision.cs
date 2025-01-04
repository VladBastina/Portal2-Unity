using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PickupCollision : MonoBehaviour
{
    public event Action<Collider> OnCollisionEvent;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger detected with {other.name}");
        OnCollisionEvent?.Invoke(other);
    }
}
