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
    private Rigidbody childRb; // �ڽ��� Rigidbody�� ������ ����
    // ������ ������� �߰� ������

    private void Awake()
    {
        enemyMovingAction = GetComponent<EnemyMovingAction>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
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

    void Dead()
    {
        gameObject.SetActive(false);
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