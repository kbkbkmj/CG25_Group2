using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;

    public void Move(Rigidbody rb, Vector3 inputVector)   
    {
        // Moving
        Vector3 dirVector = inputVector;

        rb.linearVelocity = new Vector3(dirVector.x * speed, rb.linearVelocity.y, dirVector.z * speed);
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }
}
