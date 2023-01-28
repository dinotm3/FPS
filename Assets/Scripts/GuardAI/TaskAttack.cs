using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskAttack : Node
{
    private Animator _animator;

    private Transform _lastTarget;
    private PlayerManager _playerManager;
    private float _attackTime = 1f;
    private float _attackCounter = 0f;
    private Transform _startPosition;
    private EnemyRangeCheck _rangeCheck;

    public TaskAttack(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
        _rangeCheck = _animator.GetComponentInParent<EnemyRangeCheck>();
    }

    private NodeState Attack(Transform target)
    {
        if (_rangeCheck.CheckRange(target.position))
        {
            _animator.SetBool("Attacking", true);
            _animator.SetBool("Walking", false);
            _animator.SetBool("Idle", false);
            bool enemyIsDead = _playerManager.TakeHit();
            if (enemyIsDead)
            {
                ClearData("target");
                _animator.SetBool("Attacking", false);
            }
            else
            {
                _attackCounter = 0f;
                _animator.SetBool("Attacking", true);
                _animator.SetBool("Walking", false);
                _animator.SetBool("Idle", false);
            }
        } else
        {
            _animator.SetBool("Attacking", false);
            _animator.SetBool("Walking", true);
            _animator.SetBool("Idle", false);
        }
        return state = NodeState.FAILURE;  
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target == null)
        {
            target = (Transform)GetData("target");
        }

        if (target != _lastTarget)
        {
            _playerManager = target.GetComponent<PlayerManager>();
            _lastTarget = target;
        }

        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackTime && target != null)
        {
            if (target != null)
            {
                Attack(target);
                _playerManager = target.GetComponent<PlayerManager>();

            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
