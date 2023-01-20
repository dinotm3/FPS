using System.Collections.Generic;
using BehaviorTree;

public class GuardBT : Tree
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 2f;
    public static float fovRange = 6f;
    protected override Node SetupTree()
    {
        //Node root = new TaskPatrol(transform, waypoints);

        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new FOVRangeCheck(transform),
                new TaskGoToTarget(transform),
            }),
            new TaskPatrol(transform, waypoints),
        });
        return root;
    }
}
