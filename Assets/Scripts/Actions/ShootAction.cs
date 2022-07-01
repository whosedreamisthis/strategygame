using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : BaseAction
{
    public event EventHandler<OnShootEventArgs> OnShoot;

    public class OnShootEventArgs : EventArgs
    {
        public Unit targetUnit;
        public Unit shooter;
    }

    private enum State
    {
        Aiming,
        Shooting,
        Cooloff,
    }
    private State state;
    [SerializeField] private int maxShootDistance = 6;
    private float stateTimer;
    private Unit targetUnit;
    private bool canShootBullet;

    public override string GetActionName()
    {
        return "Shoot";
    }

    private void Update()
    {
        if (!isActive) return;

        stateTimer -= Time.deltaTime;
        switch (state)
        {
            case State.Aiming:
                float rotateSpeed = 10f;
                Vector3 aimDirection = (targetUnit.GetWorldPosition() - unit.GetWorldPosition()).normalized;

                transform.forward = Vector3.Lerp(transform.forward, aimDirection, rotateSpeed * Time.deltaTime);

                break;
            case State.Shooting:
                if (canShootBullet)
                {
                    Shoot();
                    canShootBullet = false;
                }
                break;
            case State.Cooloff:

                break;
        }

        if (stateTimer <= 0f)
        {
            NextState();
        }
        //        float spinAddAmount = 360f * Time.deltaTime;

        //totalSpinAmount += spinAddAmount;
        // if (totalSpinAmount >= 360)
        //   {
        //     isActive = false;
        //     onComplete();
        // }
        // transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
    }

    private void Shoot()
    {
        targetUnit.Damage();
    }

    private void NextState()
    {
        switch (state)
        {
            case State.Aiming:

                float shootingStateTime = 0.1f;
                stateTimer = shootingStateTime;
                state = State.Shooting;
                break;
            case State.Shooting:

                float coolOffStateTime = 0.5f;
                stateTimer = coolOffStateTime;
                state = State.Cooloff;
                OnShoot?.Invoke(this, new OnShootEventArgs
                {
                    targetUnit = targetUnit,
                    shooter = unit
                });
                break;
            case State.Cooloff:

                ActionComplete();

                break;
        }
    }
    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();
        for (int x = -maxShootDistance; x <= maxShootDistance; x++)
        {
            for (int z = -maxShootDistance; z <= maxShootDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > maxShootDistance)
                {
                    continue;
                }
                if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    // Grid position already occupied with another unit.
                    continue;
                }

                Unit targetUnit = LevelGrid.Instance.GetUnitOnGridPosition(testGridPosition);

                if (targetUnit.IsEnemy() == unit.IsEnemy())
                {
                    continue;
                }


                validGridPositionList.Add(testGridPosition);
            }
        }
        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        ActionStart(onActionComplete);
        targetUnit = LevelGrid.Instance.GetUnitOnGridPosition(gridPosition);
        state = State.Aiming;
        float aimingStateTime = 1f;
        stateTimer = aimingStateTime;
        canShootBullet = true;

        //Vector3 shootDirection = (gridPosition - transform.position).normalized;


    }
}
