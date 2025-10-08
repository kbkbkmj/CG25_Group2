using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed = 5.0f;
    public Rigidbody chasingTarget;

    public void Move(Rigidbody rb)
    {
        // Direction of Moving
        Vector3 dirVector = chasingTarget.position - rb.position;
        // Next Position
        Vector3 nextVec = dirVector.normalized * speed * Time.fixedDeltaTime;

        // Move
        rb.MovePosition(rb.position + nextVec);
    }

    private void OnEnable()
    {
        chasingTarget = GameManager.instance.playerController.GetPlayerInputAction().GetRigidbody();
    }
}
