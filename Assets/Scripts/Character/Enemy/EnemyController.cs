using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyMovement enemyMovement;

    private bool isLive = true;

    private void Awake()
    {
        enemyMovement = GetComponentInChildren<EnemyMovement>();
    }

    void FixedUpdate()
    {
        //If not alive, Return
        if (!isLive) return;
        //If Alive, Do Action
        enemyMovement.Move();
    }
}
