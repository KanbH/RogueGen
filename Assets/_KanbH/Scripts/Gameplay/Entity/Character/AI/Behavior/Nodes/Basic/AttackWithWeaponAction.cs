using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AttackWithWeapon", story: "Agent attack in the direction of [Target] with equipped weapon", category: "_KanbH", id: "ca0e46b077adc057ab6e0b637829e10d")]
public partial class AttackWithWeaponAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Target;

    private EntityController _entityController;

    protected override Status OnStart()
    {
        if (_entityController == null)
        {
            _entityController = GameObject.GetComponent<EntityController>();
        }
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        _entityController.AttackAtDirection(Target.Value.position);
        return Status.Success;
    }

}

