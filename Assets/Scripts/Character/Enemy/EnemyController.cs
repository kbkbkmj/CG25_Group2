using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyMovingAction enemyMovingAction;

    [Header("�� ����")]
    public float maxHealth = 10f;
    private float currentHealth;
    private bool isAlive = true;
    [SerializeField] private float hp;
    [SerializeField] private float speed;

    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;

    public Mesh[] meshes;
    public Material[] materials;
    

    [Header("������ ������")]
    public GameObject experienceGemPrefab;

    // ������ �� ���� �߰��ϼ���! ������
    private Rigidbody rb; // Rigidbody�� ������ ����
    // ������ ������� �߰� ������

    private void Awake()
    {
        enemyMovingAction = GetComponent<EnemyMovingAction>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
        enemyMovingAction = GetComponentInChildren<EnemyMovingAction>();

        // ������ �� ���� �߰��ϼ���! (Rigidbody ã�ƿ���) ������
        rb = GetComponent<Rigidbody>();
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
        // 100% ������� �׽�Ʈ
        // if (Random.value < 0.5f) 
        // {
        DropExperienceGem();
        // }

        isAlive = false;
        GameManager.instance.poolManager.isSpawnable[0] = true;
        gameObject.SetActive(false);
    }

    private void DropExperienceGem()
    {
        GameObject gem = GameManager.instance.poolManager.GetPrefab((int)PoolManager.PoolType.ExperienceGem);

        if (gem != null)
        {
            // �� ��ġ�� �� ��ġ�� ����
            gem.transform.position = rb.position;
        }
    }

    public bool IsAlive()
    {
        return isAlive;
    }
}