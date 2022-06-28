using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GridDebugObject : MonoBehaviour
{
    private GridObject gridObject;
    [SerializeField] private TextMeshPro text;
    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
        //this.text = this.gridObject.GetGridPosition().ToString();
    }

    private void Update()
    {
        text.text = this.gridObject.ToString();
    }
}
