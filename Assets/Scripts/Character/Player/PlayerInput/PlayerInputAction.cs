using UnityEngine;

public class PlayerInputAction : MonoBehaviour
{
    private PlayerControlInput inputActions;
    private Vector3 inputVector;

    private PlayerMovement playerMovement;
    private PlayerRotation playerRotation;

    void Awake()
    {
        //Unity New Input
        inputActions = new PlayerControlInput();
        inputActions.Player.Move.performed += ctx => inputVector = ctx.ReadValue<Vector3>();
        inputActions.Player.Move.canceled += ctx => inputVector = Vector3.zero;

        //Get Component in Player Modeling
        playerMovement = GetComponentInChildren<PlayerMovement>();
        playerRotation = GetComponentInChildren<PlayerRotation>();
    }

    // Update is called once per frame
    public void GetInputAction()
    {
        playerMovement.Move(inputVector);
        playerRotation.Rotate(inputVector);
    }

    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();
}
