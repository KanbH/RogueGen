using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;
using CharlieMadeAThing.NeatoTags.Core;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SightDetectPlayer", story: "[Agent] detects [Player] within [SightRange]", category: "Action", id: "d7f6acc136ccded3091449e90181c2b1")]
public partial class SightDetectPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<float> SightRange;

    private List<RaycastHit2D> _rayHits = new List<RaycastHit2D>();
    private int _hitCount;
    private ContactFilter2D _contactFilter = new ContactFilter2D();

    protected override Status OnStart()
    {
        _contactFilter.NoFilter();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        _hitCount = Physics2D.Raycast(Agent.Value.transform.position, Player.Value.transform.position - Agent.Value.transform.position, _contactFilter, _rayHits, SightRange.Value);
        Debug.DrawRay(Agent.Value.transform.position, Player.Value.transform.position - Agent.Value.transform.position, Color.green);

        foreach (var hit in _rayHits)
        {
            if (hit.collider.gameObject.HasTag("Player"))
            {
                Debug.Log("Player detected!");
                return Status.Success;
            }
        }
        Debug.Log("Player not detected...");
        return Status.Failure;
    }
}

