using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyMovingAction enemyMovingAction;

    [Header("적 스탯")]
    public float maxHealth = 10f;
    private float currentHealth;
    private bool isAlive = true;

    [Header("아이템 프리팹")]
    public GameObject experienceGemPrefab;

    // ▼▼▼▼▼ 이 줄을 추가하세요! ▼▼▼▼▼
    private Rigidbody childRb; // 자식의 Rigidbody를 저장할 변수
    // ▲▲▲▲▲ 여기까지 추가 ▲▲▲▲▲

    private void Awake()
    {
        enemyMovingAction = GetComponentInChildren<EnemyMovingAction>();

        // ▼▼▼▼▼ 이 줄을 추가하세요! (자식의 Rigidbody 찾아오기) ▼▼▼▼▼
        childRb = GetComponentInChildren<Rigidbody>();
        // ▲▲▲▲▲ 여기까지 추가 ▲▲▲▲▲
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

        // 100% 드롭으로 테스트
        // if (Random.value < 0.5f) 
        // {
        DropExperienceGem();
        // }

        gameObject.SetActive(false);
    }

    // ▼▼▼▼▼ 이 함수 내부를 수정하세요! (this.transform.position -> childRb.position) ▼▼▼▼▼
    private void DropExperienceGem()
    {
        GameObject gem = GameManager.instance.poolManager.GetPrefab((int)GameManager.PoolType.ExperienceGem);

        if (gem != null)
        {
            // 젬 위치를 '부모' 위치가 아닌 '자식(Rigidbody)'의 위치로 설정
            gem.transform.position = childRb.position;
        }
    }
    // ▲▲▲▲▲ 여기까지 수정 ▲▲▲▲▲

    public bool IsAlive()
    {
        return isAlive;
    }
}