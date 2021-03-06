using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance
    {
        get; private set;
    }

    public event EventHandler OnAnyUnitMovedGridPosition;
    private GridSystem gridSystem;

    [SerializeField] private Transform gridDebugObjectPrefab;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("there's more than one UnitActionSystem " + transform + " - " + Instance);
            return;
        }
        Instance = this;

        gridSystem = new GridSystem(10, 10, 2);
        gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
    }

    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        gridSystem.GetGridObject(gridPosition).AddUnit(unit);
    }

    public List<Unit> GetUnitsAtGridPosition(GridPosition gridPosition)
    {
        return gridSystem.GetGridObject(gridPosition).GetUnitList();
    }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        gridSystem.GetGridObject(gridPosition).RemoveUnit(unit);
    }

    public void UnitMovedGridPosition(Unit unit, GridPosition fromPosition, GridPosition toPosition)
    {
        RemoveUnitAtGridPosition(fromPosition, unit);
        AddUnitAtGridPosition(toPosition, unit);
        OnAnyUnitMovedGridPosition?.Invoke(this, EventArgs.Empty);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);
    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

    public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);

    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition) => gridSystem.HasAnyUnitOnGridPosition(gridPosition);
    public Unit GetUnitOnGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnit();
    }

    public int GetWidth() => gridSystem.GetWidth();
    public int GetHeight() => gridSystem.GetHeight();

}
