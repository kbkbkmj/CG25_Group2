using UnityEngine;
using System.Linq;

public class PlayerAttack : MonoBehaviour
{
    [Header("공격 설정")]
    public float attackRange = 10f;
    public float attackCooldown = 1.0f;

    // ▼▼▼▼▼ 이 줄을 추가하세요! ▼▼▼▼▼
    [Header("공격 위치")]
    public Transform attackSpawnPoint; // (알림) 여기에 'Model' (자식)을 연결해야 합니다!
    // ▲▲▲▲▲ 여기까지 추가 ▲▲▲▲▲

    private float lastAttackTime;

    void Update()
    {
        // ... (Update 함수는 그대로) ...
        if (Time.time > lastAttackTime + attackCooldown)
        {
            FindAndAttackNearestEnemy();
            lastAttackTime = Time.time;
        }
    }

    void FindAndAttackNearestEnemy()
    {
        // ... (FindAndAttackNearestEnemy 함수는 그대로) ...
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closestEnemy = null;
        float minDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            if (!enemy.activeSelf) { continue; }
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy.transform;
            }
        }
        if (closestEnemy != null && minDistance <= attackRange)
        {
            Attack(closestEnemy);
        }
    }

    void Attack(Transform target)
    {
        GameObject bulletObject = GameManager.instance.poolManager.GetPrefab((int)GameManager.PoolType.PlayerBullet);

        // ▼▼▼▼▼ 이 줄을 수정하세요! (transform.position -> attackSpawnPoint.position) ▼▼▼▼▼
        bulletObject.transform.position = attackSpawnPoint.position; // 총알 위치를 '자식' 위치로 설정
        // ▲▲▲▲▲ 여기까지 수정 ▲▲▲▲▲

        PlayerBullet bulletScript = bulletObject.GetComponent<PlayerBullet>();
        if (bulletScript != null)
        {
            bulletScript.SetTarget(target);
        }
    }
}