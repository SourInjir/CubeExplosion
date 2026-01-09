using UnityEngine;
using System;

public class Raycast : MonoBehaviour
{
    [SerializeField] SystemEventChannel _eventChannel;

    private void Awake()
    {
        _eventChannel.LeftMouseClick += ClickEventHandler;
    }

    private void OnDestroy()
    {
        _eventChannel.LeftMouseClick -= ClickEventHandler;
    }

    private void ClickEventHandler()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);

        foreach (RaycastHit hit in hits)
        {

            if (hit.collider is TerrainCollider)
            {
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
