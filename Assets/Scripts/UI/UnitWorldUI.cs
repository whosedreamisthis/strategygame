using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class UnitWorldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionPointsText;
    [SerializeField] private Unit unit;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private HealthSystem healthSystem;

    private void Start()
    {
        Unit.OnAnyActionPointsChanged += Unit_OnAnyActionPointsChanged;
        healthSystem.OnDamaged += HealthSustem_OnDamaged;

        UpdateActionPointsText();
        UpdateHealthBar();
    }

    private void UpdateActionPointsText()
    {
        actionPointsText.text = unit.GetActionPoints().ToString();
    }


    private void Unit_OnAnyActionPointsChanged(object sender, EventArgs e)
    {
        Debug.Log(" Unit_OnAnyActionPointsChanged");

        UpdateActionPointsText();
    }

    private void HealthSustem_OnDamaged(object sender, EventArgs e)
    {
        UpdateHealthBar();
    }
    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = healthSystem.GetHealthNormalized();

        Debug.Log(" fill " + healthBarImage.fillAmount);
    }

}
