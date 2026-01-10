// CubeController.cs
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class CollidingObject : MonoBehaviour
{
    public void ChangeColor()
    {
        if(gameObject.TryGetComponent<Renderer>(out var renderer))
        {
            renderer.material.color = new Color(Random.value, Random.value, Random.value);
        }
    }
}