using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pet : MonoBehaviour
{
    public NavMeshAgent agent;
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }
    public void Move()
    {
        Ray movePosition = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(movePosition, out var hitInfo))
        {
            agent.SetDestination(hitInfo.point);
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Move();
        }
    }
}
