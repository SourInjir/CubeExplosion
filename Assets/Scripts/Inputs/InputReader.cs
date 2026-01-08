using UnityEngine;
using System;

public class InputReader : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private SystemEventChannel _eventChannel;

    private const int LeftMouseButton = 0;

    public event Action MouseClicked;


    private void Update()
    {
        HandleMouseClick();
    }

    private void HandleMouseClick()
    {
        if (!Input.GetMouseButtonDown(0)) 
            return;

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray,  Mathf.Infinity);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider is TerrainCollider) {
                continue;
            }
            IClickableObject clickable = hit.collider.GetComponent<IClickableObject>();
            if (clickable != null)
            {
                _eventChannel.DispatchEvent(hit.collider.gameObject);
                break;
            }
        }
    }
}
