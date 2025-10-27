// �� �ڵ� ��ü�� �����ؼ� GameManager.cs�� �������!
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Object")]
    public PlayerController playerController;
    public PoolManager poolManager;
    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    [Header("# Player Info")]
    public int hp;
    public int maxHp = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    // ������ �� ���� '�ݵ��' �߰��Ǿ�� �մϴ�! ������
    // Spawner�� 0, 1��(��)�� ���Ƿ� 2��(�Ѿ�), 3��(��)�� ���� �߰��մϴ�.
    public enum PoolType { Enemy_A, Enemy_B, PlayerBullet, ExperienceGem }
    // ������ ������� �߰� ������


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        hp = maxHp;
    }

    void Update()
    {
        // Time Update
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;

        if(exp == nextExp[level])
        {
            level++;
            exp = 0;

        }
    }
}
