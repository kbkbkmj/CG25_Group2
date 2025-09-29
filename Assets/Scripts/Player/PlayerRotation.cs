using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10.0f;

    public void Rotate(Vector3 inputVector)
    {
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.z);

        //Rotation
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}
