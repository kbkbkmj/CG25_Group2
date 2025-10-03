using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyMovingAction enemyMovingAction;

    private bool isAlive = true;

    private void Awake()
    {
        enemyMovingAction = GetComponentInChildren<EnemyMovingAction>();
    }

    // Physics -> FixedUpdate
    void FixedUpdate()
    {
        //If not alive, Return
        if (!isAlive) return;
        //If Alive, Do Action
        enemyMovingAction.GetMovingAction();
    }
}
