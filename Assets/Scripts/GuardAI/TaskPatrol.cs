using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class TaskPatrol : Node
{
    private Transform _transform;
    private Transform[] _waypoints;
    private int _currentWaypointIndex = 0;

    private float _waitTime = 1f;
    private float _waitCounter = 0f;
    private bool _waiting = false;
    private Animator _animator;
    private NavMeshAgent agent;

    public TaskPatrol(Transform transform, Transform[] waypoints)
    {
        _transform = transform;
        _waypoints = waypoints;
        _animator = transform.GetComponent<Animator>();
        agent = _transform.GetComponent<NavMeshAgent>();
    }

    public override NodeState Evaluate()
    {
        if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter < _waitTime)
                _waiting = false;
            if (_animator != null && _animator.isActiveAndEnabled && _animator.gameObject.activeInHierarchy && _animator.gameObject.activeSelf)
            {
                _animator.SetBool("Walking", true);
                _animator.SetBool("Idle", false);
                _animator.SetBool("Attacking", false);
                _animator.SetBool("Dead", false);

            }
        }

        Transform wp = _waypoints[_currentWaypointIndex];
        if(Vector3.Distance(_transform.position, wp.position) < 3f)
        {
            _transform.position = wp.position;
            _waitCounter = 0f;
            _waiting = true;
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
            if (_animator != null && _animator.isActiveAndEnabled && _animator.gameObject.activeInHierarchy && _animator.gameObject.activeSelf)
            {
                _animator.SetBool("Walking", false);
                _animator.SetBool("Idle", true);
                _animator.SetBool("Attacking", false);
                _animator.SetBool("Dead", false);
            }
        }
        else
        {
            //_transform.position = Vector3.MoveTowards(_transform.position, wp.position, GuardBT.speed * Time.deltaTime);
            //_transform.LookAt(wp.position);
            agent.SetDestination(wp.position);
            if (_animator != null && _animator.isActiveAndEnabled && _animator.gameObject.activeInHierarchy && _animator.gameObject.activeSelf)
            {
                _animator.SetBool("Walking", true);
                _animator.SetBool("Idle", false);
                _animator.SetBool("Attacking", false);
                _animator.SetBool("Dead", false);
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}

