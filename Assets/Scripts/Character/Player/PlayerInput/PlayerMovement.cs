using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 5.0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 inputVector)   
    {
        //Moving
        rb.linearVelocity = new Vector3(inputVector.x * speed, rb.linearVelocity.y, inputVector.z * speed);
    }
}
