using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
   
    [SerializeField] private float speed = 5.0f;
    [SerializeField]private float rotationSpeed = 10.0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 inputVector)
    {
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.z);

        //Moving
        rb.linearVelocity = new Vector3(inputVector.x * speed, rb.linearVelocity.y, inputVector.z * speed);

        //Rotation
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}
