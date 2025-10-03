using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed = 5.0f;
    public Rigidbody chasingTarget;

    public void Move(Rigidbody rb)
    {
        Vector3 dirVec = chasingTarget.position - rb.position;
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + nextVec);
    }
}
