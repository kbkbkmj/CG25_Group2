using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerControlInput inputActions;
    private Vector3 inputVector;
    private float speed = 5.0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        //Unity New Input
        inputActions = new PlayerControlInput();

        inputActions.Player.Move.performed += ctx => inputVector = ctx.ReadValue<Vector3>();
        inputActions.Player.Move.canceled += ctx => inputVector = Vector3.zero;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(inputVector.x * speed, rb.linearVelocity.y, inputVector.z * speed);
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
