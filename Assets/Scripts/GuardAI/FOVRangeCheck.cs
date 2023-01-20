using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class FOVRangeCheck : Node
{
    private Transform _transform;
    //private Animator _animator;

    public static int _playerLayerMask = 1 << 6;
    
    public FOVRangeCheck(Transform transform)
    {
        _transform = transform;
        //_animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(
                _transform.position, GuardBT.fovRange, _playerLayerMask);
            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);
                // _animator.SetBool("Walk", true);
                state = NodeState.SUCCESS;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;
    }
}
