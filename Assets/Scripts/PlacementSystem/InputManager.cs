using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    private Vector3 lastPosition;

    [SerializeField]
    private LayerMask placementLayerMask, backgroundLayerMask, draggableObjectLayerMask;

    public event Action OnMouseUp, OnMouseDown;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
            OnMouseUp?.Invoke();
        if (Input.GetMouseButtonDown(0))
            OnMouseDown?.Invoke();
    }

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayerMask))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    public Vector3 GetSelectedMapPositionAll()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, backgroundLayerMask))
        {
            lastPosition = hit.point;
        }

        return lastPosition;
    }

    public Vector3 GetSelectedMapPositionDot()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, draggableObjectLayerMask))
        {
            return hit.point;
        }

        return Vector3.zero;
    }
}
