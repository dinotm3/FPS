using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pet : MonoBehaviour
{
    public NavMeshAgent agent;
    private Camera camera;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        camera = Camera.main;
    }
    public void Move()
    {
        Ray movePosition = camera.ScreenPointToRay(Input.mousePosition);
        Vector3 lastMovePosition = Input.mousePosition;
        if (Physics.Raycast(movePosition, out var hitInfo))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("WalkForward", true);
            agent.SetDestination(hitInfo.point);
        }

    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Move();
        }

        if (agent.remainingDistance < 1)
        {
            animator.SetBool("WalkForward", false);
            animator.SetBool("Idle", true);
        }
    }
}
