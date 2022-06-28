using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance
    {
        get; private set;
    }
    public event EventHandler OnSelectedUnitChanged;
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("there's more than one UnitActionSystem " + transform + " - " + Instance);
            return;
        }
        Instance = this;
    }


    private void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            if (!TryHandleUnitSelection())
            {
                GridPosition mouseGridPodition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

                if (selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPodition))
                {
                    selectedUnit.GetMoveAction().Move(mouseGridPodition);
                }


            }
        }
    }

    private bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
        {
            if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);

                return true;
            }
        }
        return false;

    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
