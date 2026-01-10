using UnityEngine;

public interface IClickableObject
{
    public Renderer GetRenderer();
    public Rigidbody GetRigidbody();

    public int GetGenerationCount();
    public void IncrementGenerationCount();
}