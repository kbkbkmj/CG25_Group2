// �� �ڵ� ��ü�� �����ؼ� GameManager.cs�� �������!
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController playerController;
    public PoolManager poolManager;

    // ������ �� ���� '�ݵ��' �߰��Ǿ�� �մϴ�! ������
    // Spawner�� 0, 1��(��)�� ���Ƿ� 2��(�Ѿ�), 3��(��)�� ���� �߰��մϴ�.
    public enum PoolType { Enemy_A, Enemy_B, PlayerBullet, ExperienceGem }
    // ������ ������� �߰� ������

    private void Awake()
    {
        instance = this;
    }
}