using UnityEngine;
using System.Linq;

public class PlayerAttack : MonoBehaviour
{
    [Header("���� ����")]
    public float attackRange = 10f;
    public float attackCooldown = 1.0f;

    // ������ �� ���� �߰��ϼ���! ������
    [Header("���� ��ġ")]
    public Transform attackSpawnPoint; // (�˸�) ���⿡ 'Model' (�ڽ�)�� �����ؾ� �մϴ�!
    // ������ ������� �߰� ������

    private float lastAttackTime;

    void Update()
    {
        // ... (Update �Լ��� �״��) ...
        if (Time.time > lastAttackTime + attackCooldown)
        {
            FindAndAttackNearestEnemy();
            lastAttackTime = Time.time;
        }
    }

    void FindAndAttackNearestEnemy()
    {
        // ... (FindAndAttackNearestEnemy �Լ��� �״��) ...
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

        // ������ �� ���� �����ϼ���! (transform.position -> attackSpawnPoint.position) ������
        bulletObject.transform.position = attackSpawnPoint.position; // �Ѿ� ��ġ�� '�ڽ�' ��ġ�� ����
        // ������ ������� ���� ������

        PlayerBullet bulletScript = bulletObject.GetComponent<PlayerBullet>();
        if (bulletScript != null)
        {
            bulletScript.SetTarget(target);
        }
    }
}