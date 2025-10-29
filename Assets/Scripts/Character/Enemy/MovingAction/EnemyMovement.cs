using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody chasingTarget;

    public void Move(Rigidbody rb, float speed)
    {
        // Direction of Moving
        Vector3 dirVector = chasingTarget.position - rb.position;
        // Next Position
        Vector3 nextVec = dirVector.normalized * speed * Time.fixedDeltaTime;

        // Move
        rb.MovePosition(rb.position + nextVec);
    }

    private void Start()
    {
        chasingTarget = GameManager.instance.playerController.GetComponent<Rigidbody>();
    }
}
