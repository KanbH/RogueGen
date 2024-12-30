using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionsAdapter : MonoBehaviour
{
    private Vector2 _pointerWorldPosition;

    InputAction _moveAction;
    InputAction _lookAction;
    InputAction _attackAction;

    private EntityController _entityController;
    private EntityMovement _entityMovement;

    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");
        _attackAction = InputSystem.actions.FindAction("Attack");

        _entityController = GetComponent<EntityController>();
        _entityMovement = GetComponent<EntityMovement>();
    }

    void Update()
    {
        _pointerWorldPosition = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
        //_pointerWorldPosition = Pointer.current.position.ReadValue();

        Vector2 moveValue = _moveAction.ReadValue<Vector2>();
        _entityMovement.SetMovementDirection(moveValue);

        if (_attackAction.WasPressedThisFrame())
        {
            _entityController.AttackAtDirection(_pointerWorldPosition);
        }
    }
}
