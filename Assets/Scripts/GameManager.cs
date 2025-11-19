// 이 코드 전체를 복사해서 GameManager.cs에 덮어쓰세요!
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public float hp;
    public float maxHp = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    public LevelUp uiLevelUp;
    public GameObject uiResult;


    private void Awake()
    {
        instance = this;
        isGameStop = true;
    }

    public void GameStart()
    {
        Debug.Log("Start!");
        Time.timeScale = 1;

        hp = maxHp;
        uiLevelUp.Select(0);    //TEMP


        isGameStop = false;
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }
    IEnumerator GameOverRoutine()
    {
        isGameStop = true;

        yield return new WaitForSeconds(1.5f);

        uiResult.SetActive(true);
        GameStop();
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
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
