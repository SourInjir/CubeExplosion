// CubeController.cs
using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class Platform : MonoBehaviour
{
    [SerializeField] private SystemEventChannel _eventChannel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CollidingObject>(out var collidingObject))
        {
            collidingObject.ChangeColor();
            _eventChannel.DispatchPlatformColideEvent(other.gameObject);
        }
    }
}