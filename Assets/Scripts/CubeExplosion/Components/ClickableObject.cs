using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(Renderer))]
public class ClickableObject : MonoBehaviour, IClickableObject
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Rigidbody _rigidbody;
    
    private int generationCount = 1;

    public Renderer GetRenderer() => _renderer;
    public Rigidbody GetRigidbody() => _rigidbody;
    public int GetGenerationCount() => generationCount;

    public void IncrementGenerationCount()
    {
        generationCount++;
    }
}