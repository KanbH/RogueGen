using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChaseUntilRange", story: "Agent chases [Target] until distance is less than [AttackRange] and rechase if target is beyond [ReChaseRange]", category: "_KanbH", id: "9fb78db10a4cc2ca87760956301a1b6b")]
public partial class ChaseUntilRangeAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> AttackRange;
    [SerializeReference] public BlackboardVariable<float> ReChaseRange;

    private MoveToDestination _moveToDestination;

    private bool _hasChased = false;

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
        //Target is far away and need chasing
        if (!_hasChased)
        {
            _moveToDestination.SetTargetPosition(Target.Value.position);

            //Target is within attaack range, begin attacking the target
            if (TargetWithinAttackRange())
            {
                Debug.Log("Agent within the Range");
                _moveToDestination.ClearMovementTarget();
                _hasChased = true;
                return Status.Success;
            }
            else if (_moveToDestination.HavePath())
            {
                //Debug.Log("Agent on the way");
                return Status.Running; //Still chasing Target
            }
            else
            {
                //Debug.Log("Agent can't find path to reach player...");
                return Status.Failure;
            }
        }
        else
        {
            //Target has move far away and need rechasing
            if (TargetBeyondRange())
            {
                _hasChased = false;
                return Status.Failure;
            }
            else return Status.Success; //Target is still within outer range
        }
        
    }

    private bool TargetWithinAttackRange()
    {
        return Vector2.Distance(Target.Value.position, GameObject.transform.position) < AttackRange.Value;
    }

    private bool TargetBeyondRange()
    {
        return Vector2.Distance(Target.Value.position, GameObject.transform.position) > ReChaseRange.Value;
    }
}

