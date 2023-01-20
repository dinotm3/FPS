using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskAttack : Node
{
    //private Animator _animator;

    private Transform _lastTarget;
    private PlayerManager _playerManager;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;
    private float _attackRange = 2;
    private Transform _startPosition;

    public TaskAttack(Transform transform)
    {
        //_animator = transform.GetComponent<Animator>();
        //_startPosition = transform.GetComponent<Transform>();
        
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        //var currentPosition = _startPosition.position;
        //float distance = Vector3.Distance(currentPosition, target.position);
        //Debug.Log("distance: " + distance);

        //if (target != _lastTarget)
        //{
        //    _playerManager = target.GetComponent<PlayerManager>();
        //    _lastTarget = target;
        //}

        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackTime)
        {
            bool enemyIsDead = target.GetComponent<PlayerManager>().TakeHit();
            if (enemyIsDead)
            {
                ClearData("target");
                //_animator.SetBool("Attacking", false);
               // _animator.SetBool("Walking", true);
            }
            else
            {
                _attackCounter = 0f;
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
