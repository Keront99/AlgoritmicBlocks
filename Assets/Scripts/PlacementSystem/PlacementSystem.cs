using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectDatabaseSO database;
    [SerializeField]
    private GameObject parentObject;
    private int selectedObjectIndex = -1;

    private GridData gridData;

    private GameObject newObject;
    private List<GameObject> placedGameObjects = new();

    private void Start()
    {
        StopPlacement();
        gridData = new();
        inputManager.OnMouseDown += DragStracture;
    }


    public void StartPlacement(int ID)
    {
        StopPlacement();

        //Получение ID типа объекта
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);

        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found: {ID}");
            return;
        }

        inputManager.OnMouseUp += PlaceStructure;
        newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
        newObject.transform.parent = parentObject.transform;
        Cursor.visible = false;
    }

    private void PlaceStructure()
    {
        //Получение клетки под курсором
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);

        if (newObject && gridPosition.z == 0 && placementValidity)
        {
            //Перемещение объекта на позицию клетки, добавление объекта в списки объектов и сетки
            newObject.transform.position = grid.CellToWorld(gridPosition);
            placedGameObjects.Add(newObject);
            gridData.AddObjectAt(gridPosition,
                                 database.objectsData[selectedObjectIndex].Size,
                                 database.objectsData[selectedObjectIndex].ID,
                                 placedGameObjects.Count - 1);
        }
        else
        {
            Destroy(newObject);

        }

        newObject = null;
        inputManager.OnMouseUp -= PlaceStructure;
        Cursor.visible = true;
    }

    private void DragStracture()
    {
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        //If "on dot" check
        Vector3 mousePosition2 = inputManager.GetSelectedMapPositionDot();
        if (mousePosition2 != Vector3.zero)
        {
            return;
        }

        //Debug.Log(gridPosition);

        bool placementValidity = gridData.CanPlaceObject(gridPosition, Vector2Int.one);

        //Debug.Log(placementValidity);
        //Debug.Log(gridData);


        if (!placementValidity && gridData != null && gridPosition.z == 0)
        {
            // Получение индекса объекта по позиции в сетке
            selectedObjectIndex = gridData.GetRepresentationInfo(gridPosition);

            //Debug.Log($"Индекс объекта: {selectedObjectIndex}");

            if (selectedObjectIndex == -1)
                return;
            //Получение ссылки на объект и обнуление ссылки в списке
            newObject = placedGameObjects[selectedObjectIndex];
            placedGameObjects[selectedObjectIndex] = null;

            //Получение ID типа объекта и удаление данных из списка в сетке
            selectedObjectIndex = gridData.GetIDInfo(gridPosition);
            gridData.RemoveObjectAt(gridPosition);

            inputManager.OnMouseUp += PlaceStructure;
            Cursor.visible = false;

            //Debug.Log($"Тип объекта: {selectedObjectIndex}");
        }
        else
            return;
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        return gridData.CanPlaceObject(gridPosition, database.objectsData[selectedObjectIndex].Size);
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;

    }

    public int GetPrice()
    {
        int prevObjectIndex = -1;
        int price = 0;
        foreach (var item in gridData.PlacedObjects)
        {
            if (item.Value.PlacedObjectIndex != prevObjectIndex)
            {
                switch (item.Value.ID)
                {
                    default: price += 0; prevObjectIndex = item.Value.PlacedObjectIndex; break;
                    case 0: price += 5; prevObjectIndex = item.Value.PlacedObjectIndex; break;
                    case 1: price += 5; prevObjectIndex = item.Value.PlacedObjectIndex; break;
                    case 2: price += 10; prevObjectIndex = item.Value.PlacedObjectIndex; break;
                    case 3: price += 10; prevObjectIndex = item.Value.PlacedObjectIndex; break;
                    case 4: price += 15; prevObjectIndex = item.Value.PlacedObjectIndex; break;
                    case 5: price += 5; prevObjectIndex = item.Value.PlacedObjectIndex; break;
                }
            }
        }
        return price;
    }

    private void Update()
    {
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        bool isOnGrid = gridPosition.z == 0;

        //Debug.Log(gridPosition);

        //Привязка объекта при перемещении к клеткам сетки
        if (newObject && !isOnGrid)
        {
            newObject.transform.position = inputManager.GetSelectedMapPositionAll();
        }
        if (newObject && isOnGrid)
        {
            newObject.transform.position = grid.CellToWorld(gridPosition);
        }
        gridPosition = Vector3Int.zero;
    }
}
