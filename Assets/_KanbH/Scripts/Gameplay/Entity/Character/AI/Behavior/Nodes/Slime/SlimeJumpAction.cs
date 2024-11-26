using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SlimeJump", story: "Slime jumps toward [Target] with [Force] force every [Cooldown] seconds", category: "_KanbH", id: "1d54b40b548832e271cba4127823d183")]
public partial class SlimeJumpAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> Force;
    [SerializeReference] public BlackboardVariable<float> Cooldown;

    private Rigidbody2D _rigidbody2D;
    private Transform _slimeTransform;
    private float _lastJumpTime = 0;
    //private bool _isJumping = false;

    protected override Status OnStart()
    {
        if (_rigidbody2D == null || _slimeTransform == null)
        {
            _rigidbody2D = GameObject.GetComponent<Rigidbody2D>();
            _slimeTransform = GameObject.GetComponent<Transform>();
        }

        if (Time.time < _lastJumpTime + Cooldown)
        {
            //Debug.Log("Jump on cooldown.");
            return Status.Failure;
        }

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
            // _isJumping = true;
            Vector2 jumpDirection = (Target.Value.transform.position - _slimeTransform.position).normalized;

            _rigidbody2D.AddForce(jumpDirection * Force, ForceMode2D.Impulse);

            _lastJumpTime = Time.time;
            //_isJumping = false;

            return Status.Success;
    }

}

