using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyMovingAction enemyMovingAction;

    [Header("�� ����")]
    public float maxHealth = 10f;
    private float currentHealth;
    private bool isAlive = true;

    [Header("������ ������")]
    public GameObject experienceGemPrefab;

    // ������ �� ���� �߰��ϼ���! ������
    private Rigidbody childRb; // �ڽ��� Rigidbody�� ������ ����
    // ������ ������� �߰� ������

    private void Awake()
    {
        enemyMovingAction = GetComponentInChildren<EnemyMovingAction>();

        // ������ �� ���� �߰��ϼ���! (�ڽ��� Rigidbody ã�ƿ���) ������
        childRb = GetComponentInChildren<Rigidbody>();
        // ������ ������� �߰� ������
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        isAlive = true;
    }

    void FixedUpdate()
    {
        if (!isAlive) return;
        enemyMovingAction.GetMovingAction();
    }

    public void TakeDamage(float damage)
    {
        if (!isAlive) return;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isAlive = false;

        // 100% ������� �׽�Ʈ
        // if (Random.value < 0.5f) 
        // {
        DropExperienceGem();
        // }

        gameObject.SetActive(false);
    }

    // ������ �� �Լ� ���θ� �����ϼ���! (this.transform.position -> childRb.position) ������
    private void DropExperienceGem()
    {
        GameObject gem = GameManager.instance.poolManager.GetPrefab((int)GameManager.PoolType.ExperienceGem);

        if (gem != null)
        {
            // �� ��ġ�� '�θ�' ��ġ�� �ƴ� '�ڽ�(Rigidbody)'�� ��ġ�� ����
            gem.transform.position = childRb.position;
        }
    }
    // ������ ������� ���� ������

    public bool IsAlive()
    {
        return isAlive;
    }
}