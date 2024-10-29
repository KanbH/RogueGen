using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using static UnityEngine.GraphicsBuffer;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Chase", story: "Agent chases [Player]", category: "_KanbH", id: "fb07993a10f7669a1583e22067b7395b")]
public partial class ChaseAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Player;

    private MoveToDestination _moveToDestination;

    protected override Status OnStart()
    {
        if (_moveToDestination == null)
        {
            _moveToDestination = GameObject.GetComponent<MoveToDestination>();
        }
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        _moveToDestination.SetTargetPosition(Player.Value.transform.position);

        if (_moveToDestination.HavePath())
        {
            if (_moveToDestination.IsOnTheWay())
            {
                Debug.Log("Agent on the way");
                return Status.Running;
            }
            else
            {
                Debug.Log("Agent reached player!");
                return Status.Success;
            }
        }
        else
        {
            Debug.Log("Agent can't find path to reach player...");
            return Status.Failure;
        }
    }
}

