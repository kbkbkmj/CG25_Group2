using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyMovingAction enemyMovingAction;

    [Header("Enemy Status")]
    public float maxHealth = 10f;
    private float currentHealth;
    private bool isAlive = true;
    [SerializeField] private float hp;
    [SerializeField] private float speed;

    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;

    public Mesh[] meshes;
    public Material[] materials;
    

    [Header("아이템 프리팹")]
    public GameObject experienceGemPrefab;

    // ▼▼▼▼▼ 이 줄을 추가하세요! ▼▼▼▼▼
    private Rigidbody rb; // Rigidbody를 저장할 변수
    // ▲▲▲▲▲ 여기까지 추가 ▲▲▲▲▲

    private void Awake()
    {
        enemyMovingAction = GetComponent<EnemyMovingAction>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
        enemyMovingAction = GetComponentInChildren<EnemyMovingAction>();

        // ▼▼▼▼▼ 이 줄을 추가하세요! (Rigidbody 찾아오기) ▼▼▼▼▼
        rb = GetComponent<Rigidbody>();
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
        //If Alive, Do Action
        enemyMovingAction.GetMovingAction(speed);
    }

    /*
    private void OnEnable()
    {
        // If Respawn, Reset Everything
        isAlive = true;
        hp = enemyStatus.MaxHp;
        speed = enemyStatus.Speed;
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        //Trigger Filter - Is it Bullet? or Is Enemy Dead?
        if (!other.CompareTag("Bullet") || !isAlive)
        {
            return;
        }
        else
        {
            //Damage HP
            hp -= other.GetComponent<Bullet>().damage;
            Debug.Log("Hit!");

            //Alive
            if (hp > 0)
            {
                //Hit Action
            }
            //Die
            else
            {
                Dead();
                GameManager.instance.kill++;
                GameManager.instance.GetExp();
            }
        }


    }

    public void Init(EnemyStatus status)
    {
        // If Respawn, Reset Everything
        isAlive = true;
        hp = status.MaxHp;
        speed = status.Speed;
        meshRenderer.material = materials[status.ModelingType];
        meshFilter.sharedMesh = meshes[status.ModelingType];
    }

    /*
    public void TakeDamage(float damage)
    {
        if (!isAlive) return;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Dead();
        }
    }
    */

    private void Dead()
    {
        Debug.Log("Dead!");
        // 100% 드롭으로 테스트
        // if (Random.value < 0.5f) 
        // {
        DropExperienceGem();
        // }

        isAlive = false;
        GameManager.instance.poolManager.elements[(int)PoolManager.PoolType.Enemy].IsSpawnable = true;
        gameObject.SetActive(false);
    }

    private void DropExperienceGem()
    {
        GameObject gem = GameManager.instance.poolManager.GetPrefab((int)PoolManager.PoolType.ExperienceGem);

        if (gem != null)
        {
            // 젬 위치를 적 위치로 설정
            gem.transform.position = rb.position;
        }
    }

    public bool IsAlive()
    {
        return isAlive;
    }
}