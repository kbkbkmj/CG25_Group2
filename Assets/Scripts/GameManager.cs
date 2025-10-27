// 이 코드 전체를 복사해서 GameManager.cs에 덮어쓰세요!
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController playerController;
    public PoolManager poolManager;

    // ▼▼▼▼▼ 이 줄이 '반드시' 추가되어야 합니다! ▼▼▼▼▼
    // Spawner가 0, 1번(적)을 쓰므로 2번(총알), 3번(젬)을 새로 추가합니다.
    public enum PoolType { Enemy_A, Enemy_B, PlayerBullet, ExperienceGem }
    // ▲▲▲▲▲ 여기까지 추가 ▲▲▲▲▲

    private void Awake()
    {
        instance = this;
    }
}