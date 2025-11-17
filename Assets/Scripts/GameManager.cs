using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Object")]
    public PlayerController playerController;
    public PoolManager poolManager;
    public WeaponLocation weaponLocation;

    // ▼▼▼▼▼ 이 줄을 추가하세요! ▼▼▼▼▼
    [Header("# Weapon Info")]
    public Weapon daggerWeapon; // (알림) 여기에 'DaggerWeapon' 오브젝트를 연결해야 합니다!
    // ▲▲▲▲▲

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

    // GameManager.cs

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[level])
        {
            level++;
            exp = 0;

            // ▼▼▼▼▼ 이 부분을 수정했습니다! ▼▼▼▼▼
            if (daggerWeapon != null)
            {
                // 1. 단검이 아직 꺼져있다면? (처음 얻는 경우) -> 켜줍니다!
                if (daggerWeapon.gameObject.activeSelf == false)
                {
                    daggerWeapon.gameObject.SetActive(true);
                    Debug.Log("단검 획득!");
                }
                // 2. 단검이 이미 켜져있다면? (이미 가지고 있는 경우) -> 레벨업!
                else
                {
                    // Weapon.cs 내부에서 id=2일 때 알아서 처리하므로 0,0을 넘깁니다.
                    daggerWeapon.LevelUp(0, 0);
                    Debug.Log("단검 레벨업!");
                }
            }
            // ▲▲▲▲▲
        }
    }
}
