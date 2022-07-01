using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit unit;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectionChange;
        UpdateVisual();
    }

    private void UnitActionSystem_OnSelectionChange(object sender, EventArgs empty)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        meshRenderer.enabled = (UnitActionSystem.Instance.GetSelectedUnit() == unit);
        //  Debug.Log("meshRenderer.enabled " + meshRenderer.enabled + " " + this);
    }

    private void OnDestroy()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged -= UnitActionSystem_OnSelectionChange;

    }
}
