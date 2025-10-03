using UnityEngine;

public class EnemyMovingAction : MonoBehaviour
{
    private Rigidbody rb;
    private EnemyMovement enemyMovement;
    private EnemyRotation enemyRotation;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody>();
        enemyMovement = GetComponentInChildren<EnemyMovement>();
        enemyRotation = GetComponentInChildren<EnemyRotation>();
    }

    public void GetMovingAction()
    {
        enemyMovement.Move(rb);
        enemyRotation.Rotate(rb);
    }
}
