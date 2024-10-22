using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;
using CharlieMadeAThing.NeatoTags.Core;

[AddComponentMenu("")]
[MBTNode(name = "RogueGen/SightDetectPlayer")]
public class SightDetectPlayer : Leaf
{
    private List<RaycastHit2D> _rayHits = new List<RaycastHit2D>();
    private int _hitCount;
    private ContactFilter2D _contactFilter = new ContactFilter2D();

    public TransformReference SelfTranform;
    public TransformReference PlayerTransform;
    public float SightRange;

    public override void OnEnter()
    {
        _contactFilter.NoFilter();
    }

    public override NodeResult Execute()
    {
        _hitCount = Physics2D.Raycast(SelfTranform.Value.position, PlayerTransform.Value.position - SelfTranform.Value.position, _contactFilter, _rayHits, SightRange);
        Debug.DrawRay(SelfTranform.Value.position, PlayerTransform.Value.position - SelfTranform.Value.position, Color.green);

        
        foreach (var hit in _rayHits )
        {
            if (hit.collider.gameObject.HasTag("Player"))
            {
                return NodeResult.success;
            }
        }
        return NodeResult.failure;
    }
}
