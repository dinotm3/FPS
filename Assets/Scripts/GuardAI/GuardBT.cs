using System.Collections.Generic;

using BehaviorTree;

public class GuardBT : Tree
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 2f;
    public static float fovRange = 10f;
    public static float attackRange = 2f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new EnemyFOVRangeCheck(transform),
                new TaskGoToTarget(transform),
                new TaskAttack(transform),
            }),
            //new Sequence(new List<Node>
            //{
            //    new FOVRangeCheck(transform),
            //    new TaskGoToTarget(transform),
            //}),
            new TaskPatrol(transform, waypoints),
        });
        return root;
    }

}
