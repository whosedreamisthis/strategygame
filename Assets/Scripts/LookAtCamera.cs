using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private bool invert;
    // Start is called before the first frame update
    private Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (invert)
        {
            Vector3 dirToCamera = (cameraTransform.position - transform.position).normalized;
            dirToCamera.y = 0;

            transform.LookAt(transform.position + dirToCamera * -1);

        }
        else
        {
            transform.LookAt(cameraTransform);
        }
    }
}
