using UnityEngine;

public class EnemyMovingAction : MonoBehaviour
{
    private Rigidbody rb;
    private EnemyMovement enemyMovement;
    private EnemyRotation enemyRotation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyRotation = GetComponent<EnemyRotation>();
    }

    public void GetMovingAction(float speed)
    {
        enemyMovement.Move(rb, speed);
        enemyRotation.Rotate(rb);
    }
}
