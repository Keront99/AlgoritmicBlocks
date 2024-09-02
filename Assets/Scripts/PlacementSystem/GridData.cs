using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    Dictionary<Vector3Int, PlacementData> placedObjects = new();

    public Dictionary<Vector3Int, PlacementData> PlacedObjects
    {
        get => placedObjects;
    }

    public void AddObjectAt(Vector3Int gridPosition,
                            Vector2Int objectSize,
                            int ID,
                            int placedObjectIndex)
    {
        List<Vector3Int> positionsToOccupy = CalculatePositions(gridPosition, objectSize);
        PlacementData data = new(positionsToOccupy, ID, placedObjectIndex);
        foreach (var position in positionsToOccupy)
        {
            if (placedObjects.ContainsKey(position))
                throw new Exception($"Dictionary already contains {position}");
            placedObjects[position] = data;
        }
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> returnVal = new();
        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                returnVal.Add(gridPosition + new Vector3Int(x, y, 0));
            }
        }
        return returnVal;
    }

    public bool CanPlaceObject(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> positionsToOccupy = CalculatePositions(gridPosition, objectSize);
        foreach ( var pos in positionsToOccupy)
        {
            if (placedObjects.ContainsKey(pos))
                return false;
        }
        return true;
    }

    internal int GetRepresentationInfo(Vector3Int gridPosition)
    {
        if (placedObjects.ContainsKey(gridPosition) == false)
            return -1;
        return placedObjects[gridPosition].PlacedObjectIndex;
    }

    internal int GetIDInfo(Vector3Int gridPosition)
    {
        if (placedObjects.ContainsKey(gridPosition) == false)
            return -1;
        return placedObjects[gridPosition].ID;
    }

    internal void RemoveObjectAt(Vector3Int gridPosition)
    {
        foreach (var pos in placedObjects[gridPosition].occupiedPositions)
        {
            placedObjects.Remove(pos);
        }
    }
}

public class PlacementData
{
    public List<Vector3Int> occupiedPositions;
    public int ID { get; private set; }
    public int PlacedObjectIndex { get; private set; }

    public PlacementData(List<Vector3Int> occupiedPositions, int iD, int placedObjectIndex)
    {
        this.occupiedPositions = occupiedPositions;
        ID = iD;
        PlacedObjectIndex = placedObjectIndex;
    }
}