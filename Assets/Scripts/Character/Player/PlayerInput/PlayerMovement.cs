using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;

    public void Move(Rigidbody rb, Vector3 inputVector)   
    {
        //Moving
        rb.linearVelocity = new Vector3(inputVector.x * speed, rb.linearVelocity.y, inputVector.z * speed);
    }
}
