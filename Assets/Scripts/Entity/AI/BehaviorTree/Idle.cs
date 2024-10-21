using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;

[AddComponentMenu("")]
[MBTNode(name = "RogueGen/Idle")]
public class Idle : Leaf
{
    private MoveToDestination _moveToDestination;

    public GameObjectReference SelfObject;

    public override void OnEnter()
    {
        _moveToDestination = SelfObject.Value.GetComponent<MoveToDestination>();
    }

    public override NodeResult Execute()
    {
        _moveToDestination.SetIdle();

        return NodeResult.success;
    }

}
