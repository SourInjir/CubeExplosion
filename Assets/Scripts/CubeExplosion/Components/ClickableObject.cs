using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(Renderer))]
public class ClickableObject : MonoBehaviour, IClickableObject
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Rigidbody _rigidbody;

    public Renderer GetRenderer() => _renderer;
    public Rigidbody GetRigidbody() => _rigidbody;
}