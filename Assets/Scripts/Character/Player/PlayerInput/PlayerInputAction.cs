using UnityEngine;

public class PlayerInputAction : MonoBehaviour
{
    private PlayerControlInput inputActions;
    private Vector3 inputVector;

    private Rigidbody rb;
    private PlayerMovement playerMovement;
    private PlayerRotation playerRotation;

    void Awake()
    {
        // Unity New Input
        inputActions = new PlayerControlInput();
        inputActions.Player.Move.performed += ctx => inputVector = ctx.ReadValue<Vector3>().normalized;
        inputActions.Player.Move.canceled += ctx => inputVector = Vector3.zero;

        // Get Component in Player Modeling
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
        playerRotation = GetComponent<PlayerRotation>();
    }

    public void GetInputAction()
    {
        playerMovement.Move(rb, inputVector);
        playerRotation.Rotate(rb, inputVector);
    }

    public Vector3 GetInputVector()
    {
        return inputVector;
    }

    public PlayerMovement GetPlayerMovement()
    {
        return playerMovement;
    }

    public PlayerRotation GetPlayerRotation()
    {
        return playerRotation;
    }

    public Rigidbody GetRigidbody()
    {
        return rb;
    }

    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();
}
