using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    public delegate void SpinCompleteDelegate();
    // Start is called before the first frame update
    private float totalSpinAmount;
    public void Spin(Action onSpinComplete)
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
}
