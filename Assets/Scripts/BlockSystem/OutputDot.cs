using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(LineRenderer))]

public class OutputDot : MonoBehaviour, IDotOutput
{
    private List<InputDot> inputDots = new();
    private InputDot inputDot;
    private LineRenderer lineRenderer;
    private InputManager inputManager;
    private bool isDragged;

    [SerializeField]
    private LayerMask outputDotLayer, inputDotLayer;
    private float numberValue;

    public List<InputDot> InputDots
    {
        get => inputDots;
    }
    public float NumberValue
    {
        get => numberValue;
    }

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        inputManager = GetComponentInParent<InputManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 100, outputDotLayer);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                //Debug.Log("Начало перетаскивания");
                isDragged = true;
                Vector3 mousePosition = inputManager.GetSelectedMapPositionAll();
                lineRenderer.SetPosition(0, mousePosition);
            }
        }
            
        if (isDragged)
        {
            //Debug.Log("Перетаскивание");
            Vector3 mousePosition = inputManager.GetSelectedMapPositionAll();
            lineRenderer.SetPosition(1, mousePosition);
        }
        if (Input.GetMouseButtonUp(0) && isDragged)
        {
            //Debug.Log("Конец перетаскивания");
            isDragged = false;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 1;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 100, inputDotLayer);
            if (hit.collider != null && hit.collider.TryGetComponent(out inputDot))
            {
                inputDots.Add(inputDot);
                inputDot.SetActiveDot(gameObject);
                inputDots[inputDots.Count - 1].OnOccupied += RemoveDot;
                inputDot = null;
            }

            lineRenderer.positionCount = 0;
            lineRenderer.positionCount = 2;
        }
    }

    private void RemoveDot(InputDot dot)
    {
        inputDots[inputDots.FindIndex(x => x == dot)].OnOccupied -= RemoveDot;
        inputDots.Remove(dot);
    }

    public void Send(float input)
    {
        if (inputDots == null)
            return;
        foreach (IDotInput dotObject in inputDots)
        {
            //Debug.Log($"Отправлено {input} в {dotObject}");
            dotObject.Activate(input);
        }
    }
}
