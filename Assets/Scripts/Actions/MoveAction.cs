using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MoveAction : BaseAction
{

    public event EventHandler OnStartMoving;
    public event EventHandler OnStopMoving;

    [SerializeField] private int maxMoveDistance = 4;
    Vector3 targetPosition;
    Action onMoveComplete;

    protected override void Awake()
    {
        base.Awake();
        targetPosition = transform.position;

    }
    public override void TakeAction(GridPosition gridPosition, Action onMoveComplete)
    {
        ActionStart(onMoveComplete);
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        OnStartMoving?.Invoke(this, EventArgs.Empty);
    }

    public void Update()
    {
        if (!isActive)
        {
            return;
        }
        float minDistance = 0.1f;
        float distance = Vector3.Distance(targetPosition, transform.position);
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        if (distance > minDistance)
        {
            float moveSpeed = 4f;

            transform.position += moveDirection * Time.deltaTime * moveSpeed;
        }
        else
        {
            OnStopMoving?.Invoke(this, EventArgs.Empty);

            ActionComplete();
        }

        float rotateSpeed = 10f;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);

    }


    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();
        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                if (unitGridPosition == testGridPosition)
                {
                    // same grid position where unit is already at.
                    continue;
                }

                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    // Grid position already occupied with another unit.
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }
        return validGridPositionList;
    }

    public override string GetActionName()
    {
        return "Move";
    }
}
