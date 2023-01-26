using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class TaskGoToTarget : Node
{
    private Transform _transform; 
    private NavMeshAgent agent;
    private Animator _animator;


    public TaskGoToTarget(Transform transform)
    {
        _transform = transform;
        agent = _transform.GetComponent<NavMeshAgent>();
        _animator = _transform.GetComponent<Animator>();

    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (target != null)
        {
            if (Vector3.Distance(_transform.position, target.position) > 0.09f)
            {
                //_transform.position = Vector3.MoveTowards(
                //    _transform.position, target.position, GuardBT.speed * Time.deltaTime);
                //_transform.LookAt(target.position);
                agent.SetDestination(target.position);
                agent.stoppingDistance = 3f;
                if (_animator != null && _animator.isActiveAndEnabled && _animator.gameObject.activeInHierarchy && _animator.gameObject.activeSelf)
                {
                    _animator.SetBool("Walking", false);
                    _animator.SetBool("Idle", false);
                }


            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
