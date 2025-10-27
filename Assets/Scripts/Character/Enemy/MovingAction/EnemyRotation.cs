using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float rotationSpeed = 10.0f;

    public void Rotate(Rigidbody rb)
    {
        float rotateX = targetTransform.position.x - rb.position.x;
        float rotateZ = targetTransform.position.z - rb.position.z;

        Vector3 moveDirection = new Vector3(rotateX, 0, rotateZ);

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    private void Start()
    {
        targetTransform = GameManager.instance.playerController.GetPlayerInputAction().GetRigidbody().GetComponent<Transform>();
    }
}
