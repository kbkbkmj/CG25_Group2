using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController playerController;

    public PoolManager poolManager;

    private void Awake()
    {
        instance = this;
    }
}
