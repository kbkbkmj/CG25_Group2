// 이 코드 전체를 복사해서 GameManager.cs에 덮어쓰세요!
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Object")]
    public PlayerController playerController;
    public PoolManager poolManager;
    public WeaponLocation weaponLocation;
    [Header("# Game Control")]
    public bool isGameStop;
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    [Header("# Player Info")]
    public int hp;
    public int maxHp = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    public LevelUp uiLevelUp;


    private void Awake()
    {
        instance = this;
        isGameStop = true;
    }

    public void GameStart()
    {
        hp = maxHp;
        uiLevelUp.Select(0);    //TEMP
        isGameStop = false;
    }

    void Update()
    {
        if (isGameStop)
        {
            return;
        }

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

        if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void GameStop()
    {
        isGameStop = true;
        Time.timeScale = 0;
    }

    public void GameResume()
    {
        isGameStop = false;
        Time.timeScale = 1;
    }
}
