using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance
    {
        get; private set;
    }
    public event EventHandler OnSelectedUnitChanged;
    public event EventHandler OnSelectedActionChanged;

    [SerializeField] private Unit selectedUnit;
    private BaseAction selectedAction;
    [SerializeField] private LayerMask unitLayerMask;

    private bool isBusy;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("there's more than one UnitActionSystem " + transform + " - " + Instance);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        SetSelectedUnit(selectedUnit);
    }

    private void HandleSelectedAction()
    {

        if (Input.GetMouseButtonDown(0))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            if (selectedAction.IsValidActionGridPosition(mouseGridPosition))
            {
                selectedAction.TakeAction(mouseGridPosition, ClearBusy);
            }
        }
    }
    private void SetBusy()
    {
        isBusy = true;
    }
    private void ClearBusy()
    {
        isBusy = false;
    }

    private void Update()
    {
        if (isBusy)
        {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (TryHandleUnitSelection())
        {
            return;
        }

        HandleSelectedAction();
    }

    private bool TryHandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
            {
                if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    if (selectedUnit != unit)
                    {
                        SetSelectedUnit(unit);
                        return true;

                    }

                }
            }
        }
        return false;

    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        SetSelectedAction(unit.GetMoveAction());
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetSelectedAction(BaseAction baseAction)
    {
        selectedAction = baseAction;
        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);

    }
    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

    public BaseAction GetSelectedAction()
    {
        return selectedAction;
    }
}
