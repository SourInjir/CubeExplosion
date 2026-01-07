using UnityEngine;
using System;

public class InputReader : MonoBehaviour
{
    private const int LeftMouseButton = 0;

    public event Action MouseClicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            MouseClicked?.Invoke();
        }
    }
}
