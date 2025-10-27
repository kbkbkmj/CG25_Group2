// 이 코드 전체를 복사해서 GameManager.cs에 덮어쓰세요!
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
    // ▼▼▼▼▼ 이 줄이 '반드시' 추가되어야 합니다! ▼▼▼▼▼
    // Spawner가 0, 1번(적)을 쓰므로 2번(총알), 3번(젬)을 새로 추가합니다.
    public enum PoolType { Enemy_A, Enemy_B, PlayerBullet, ExperienceGem }
    // ▲▲▲▲▲ 여기까지 추가 ▲▲▲▲▲


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
