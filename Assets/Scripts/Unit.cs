using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator unitAnimator;
    private Vector3 targetPosition;

    private void Awake()
    {
        targetPosition = transform.position;
    }
    private void Update()
    {
        float minDistance = 0.1f;
        float distance = Vector3.Distance(targetPosition, transform.position);
        float moveSpeed = 4f;
        if (distance > minDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
            float rotateSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);
        }
        else
        {
            unitAnimator.SetBool("IsWalking", false);
        }


    }
    public void Move(Vector3 targetPosition)
    {
        unitAnimator.SetBool("IsWalking", true);
        this.targetPosition = targetPosition;
    }
}
