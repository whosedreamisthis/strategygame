using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    public delegate void SpinCompleteDelegate();
    // Start is called before the first frame update
    private float totalSpinAmount;
    public override void TakeAction(GridPosition gridPosition, Action onSpinComplete)
    {
        onComplete = onSpinComplete;
        isActive = true;
        totalSpinAmount = 0;
    }

    private void Update()
    {
        if (!isActive) return;

        float spinAddAmount = 360f * Time.deltaTime;
        totalSpinAmount += spinAddAmount;
        if (totalSpinAmount >= 360)
        {
            isActive = false;
            onComplete();
        }
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
    }

    public override string GetActionName()
    {
        return "Spin";
    }

    public override int GetActionPointsCost()
    {
        return 2;
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();

        return new List<GridPosition> {
            unitGridPosition
        };


    }
}
