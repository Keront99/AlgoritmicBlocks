using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(LineRenderer))]

public class InputDot : MonoBehaviour, IDotInput
{
    public event Action OnInputGet;
    public event Action<InputDot> OnOccupied;
    private GameObject dot;
    private LineRenderer lineRenderer;
    private float numberValue = 0;
    private bool isSended;

    public bool IsSended
    {
        get => isSended;
    }

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 4;
    }

    private void Update()
    {
        if (dot != null && (gameObject.transform.position != lineRenderer.GetPosition(0) || dot.transform.position != lineRenderer.GetPosition(lineRenderer.positionCount - 1)))
        {
            if(dot.transform.position.x < gameObject.transform.position.x)
            {
                lineRenderer.positionCount = 4;
                lineRenderer.SetPosition(0, gameObject.transform.position);
                lineRenderer.SetPosition(1, new Vector3((dot.transform.position.x + gameObject.transform.position.x) / 2F, gameObject.transform.position.y, 4));
                lineRenderer.SetPosition(2, new Vector3((dot.transform.position.x + gameObject.transform.position.x) / 2F, dot.transform.position.y, 4));
                lineRenderer.SetPosition(3, dot.transform.position);
            }
            else
            {
                lineRenderer.positionCount = 6;
                lineRenderer.SetPosition(0, gameObject.transform.position);
                lineRenderer.SetPosition(1, new Vector3(gameObject.transform.position.x - 0.3F, gameObject.transform.position.y, 4));
                lineRenderer.SetPosition(2, new Vector3(gameObject.transform.position.x - 0.3F, (dot.transform.position.y + gameObject.transform.position.y) / 2F, 4));
                lineRenderer.SetPosition(3, new Vector3(dot.transform.position.x + 0.3F, (dot.transform.position.y + gameObject.transform.position.y) / 2F, 4));
                lineRenderer.SetPosition(4, new Vector3(dot.transform.position.x + 0.3F, dot.transform.position.y, 4));
                lineRenderer.SetPosition(5, dot.transform.position);
            }
        }

        if (dot == null)
        {
            lineRenderer.positionCount = 0;
            lineRenderer.positionCount = 4;
        }
    }

    public void Reset()
    {
        isSended = false;
        numberValue = 0;
    }

    private void OnDestroy()
    {
        OnOccupied?.Invoke(this);
    }

    public float NumberValue
    {
        get => numberValue;
    }

    public void Activate(float numVal)
    {
        numberValue = numVal;
        isSended = true;
        OnInputGet?.Invoke();
    }

    public void SetActiveDot(GameObject newDot)
    {
        if (dot != null && dot != newDot)
            OnOccupied?.Invoke(this);
        dot = newDot;
    }

}
