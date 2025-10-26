using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyMovingAction enemyMovingAction;

    private bool isAlive = true;
    [SerializeField] private float hp;
    [SerializeField] private float speed;

    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;

    public Mesh[] meshes;
    public Material[] materials;
    

    private void Awake()
    {
        enemyMovingAction = GetComponent<EnemyMovingAction>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
    }

    // Physics -> FixedUpdate
    void FixedUpdate()
    {
        //If not alive, Return
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
}
