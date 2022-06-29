using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class BaseAction : MonoBehaviour
{
    // Start is called before the first frame update
    protected Unit unit;
    protected bool isActive;
    protected Action onComplete;

    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }
}
