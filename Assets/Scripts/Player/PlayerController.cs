using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerControlInput inputActions;
    private Vector3 inputVector;

    void Awake()
    {
        playerMovement = GetComponentInChildren<PlayerMovement>();

        //Unity New Input
        inputActions = new PlayerControlInput();
        inputActions.Player.Move.performed += ctx => inputVector = ctx.ReadValue<Vector3>();
        inputActions.Player.Move.canceled += ctx => inputVector = Vector3.zero;
    }

    void FixedUpdate()
    {
        playerMovement.Move(inputVector);
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
