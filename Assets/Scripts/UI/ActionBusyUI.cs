using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBusyUI : MonoBehaviour
{
    private void Start()
    {
        UnitActionSystem.Instance.OnBusyChanged += ActionBusyUI_OnBusyChanged;
        Hide();
    }
    // Start is called before the first frame update
    public void Show()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ActionBusyUI_OnBusyChanged(object sender, bool isBusy)
    {
        if (isBusy)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}
