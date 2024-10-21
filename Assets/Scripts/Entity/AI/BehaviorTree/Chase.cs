using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;

[AddComponentMenu("")]
[MBTNode(name = "RogueGen/Chase")]
public class Chase : Leaf
{
    private MoveToDestination _moveToDestination;

    public TransformReference Target;
    public GameObjectReference SelfObject;

    public override void OnEnter()
    {
        _moveToDestination = SelfObject.Value.GetComponent<MoveToDestination>();
    }

    public override NodeResult Execute()
    {

        _moveToDestination.SetTargetPosition(Target.Value.position);

        if (_moveToDestination.HavePath())
        {
            if (_moveToDestination.IsOnTheWay())
            {
                return NodeResult.running;
            }
            else return NodeResult.success;
        }
        else return NodeResult.failure;
    }

}
