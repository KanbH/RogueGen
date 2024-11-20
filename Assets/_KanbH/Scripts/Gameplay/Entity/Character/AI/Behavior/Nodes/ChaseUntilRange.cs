using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChaseUntilRange", story: "Agent chases [Target] until distance is less than [Range]", category: "_KanbH", id: "9fb78db10a4cc2ca87760956301a1b6b")]
public partial class ChaseUntilRangeAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> Range;
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
        if (TargetWithinRange())
        {
            Debug.Log("Agent within the Range");
            _moveToDestination.ClearMovementTarget();
            return Status.Success;
        }
        else if (_moveToDestination.HavePath())
        {
            //Debug.Log("Agent on the way");
            return Status.Running;
        }
        else
        {
            //Debug.Log("Agent can't find path to reach player...");
            return Status.Failure;
        }
    }

    private bool TargetWithinRange()
    {
        return Vector2.Distance(Player.Value.transform.position,GameObject.transform.position) < Range.Value;
    }
}

