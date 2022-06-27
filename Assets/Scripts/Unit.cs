using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector3 targetPosition;

    private void Update()
    {
        float stoppingDistance = 0.1f;
        float distance = Vector3.Distance(targetPosition, transform.position);
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        float moveSpeed = 4f;
        if (distance > stoppingDistance)
        {
            transform.position += moveSpeed * moveDirection * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Move(new Vector3(4, 0, 4));
        }
    }

    private void Move(Vector3 targetPosition)
    {

        this.targetPosition = targetPosition;
    }
}
