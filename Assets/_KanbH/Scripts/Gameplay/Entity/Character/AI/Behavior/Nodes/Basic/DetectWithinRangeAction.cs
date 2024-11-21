using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;
using CharlieMadeAThing.NeatoTags.Core;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DetectWithinRange", story: "Agent detects [Target] within [DetectRange]", category: "_KanbH", id: "af85eccbb6fde60fe7c4a3818da2f9cc")]
public partial class DetectWithinRangeAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> DetectRange;

    private Transform _selfTransform;

    private List<RaycastHit2D> _rayHits = new List<RaycastHit2D>();
    private int _hitCount;
    private ContactFilter2D _contactFilter = new ContactFilter2D();

    protected override Status OnStart()
    {
        if (_selfTransform == null)
        {
            _selfTransform = GameObject.GetComponent<Transform>();
        }
        _contactFilter.NoFilter();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        _hitCount = Physics2D.Raycast(_selfTransform.position, Target.Value.position - _selfTransform.position, _contactFilter, _rayHits, DetectRange.Value);
        Debug.DrawRay(_selfTransform.position, Target.Value.position - _selfTransform.position, Color.green);

        foreach (var hit in _rayHits)
        {
            if (hit.collider.gameObject.HasTag("Player"))
            {
                //Debug.Log("Player detected!");
                return Status.Success;
            }
        }
        //Debug.Log("Player not detected...");
        return Status.Failure;
    }

}

