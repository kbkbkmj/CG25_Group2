using UnityEngine;
using System.Linq;

public class PlayerEnemyScan : MonoBehaviour
{
    private float lastAttackTime;
    [Header("Scan Option")]
    public float scanRange = 10f;
    public LayerMask targetLayer;
    public Transform closestEnemy;

    [Header("Attack Option")]
    public float attackCooldown = 1.0f;
    public Transform attackSpawnPoint;

    

    private void Update()
    {
        if (GameManager.instance.isGameStop)
        {
            return;
        }

        closestEnemy = FindEnemy();
    }

    private Transform FindEnemy()
    {
        // Find Colliders in Sphere Range
        Collider[] enemies = Physics.OverlapSphere(transform.position, scanRange, targetLayer);

        Transform closestEnemy = null;  // Target Transform
        float minDistance = scanRange * scanRange; // "Pow" is Faster than "Sqrt"

        // Finding the Closest Enemy (-> Complexity = O(k))
        foreach (var enemy in enemies)
        {
            // distance (using Vector3.sqrMagnitude)
            Vector3 direction = enemy.transform.position - transform.position;
            float distance = direction.sqrMagnitude; // distance (using Pow)

            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;

        /*
        if (closestEnemy != null)
        {
            Attack(closestEnemy);
        }
        */
    }

    void Attack(Transform target)
    {
        GameObject bulletObject = GameManager.instance.poolManager.GetPrefab((int)PoolManager.PoolType.RemoteWeapon);
        bulletObject.transform.position = attackSpawnPoint.position; // 총알 위치 설정

        RemoteWeapon bulletScript = bulletObject.GetComponent<RemoteWeapon>();
        if (bulletScript != null)
        {
            bulletScript.SetTarget(target);
        }
    }
}