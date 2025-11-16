using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputAction playerInputAction;
    private PlayerEnemyScan playerEnemyScan;
    public GameObject weaponLocation;

    void Awake()
    {
        playerInputAction = GetComponent<PlayerInputAction>();
        playerEnemyScan = GetComponent<PlayerEnemyScan>();
    }

    //Physics -> FixedUpdate
    void FixedUpdate()
    {
        if (GameManager.instance.isGameStop)
        {
            return;
        }

        playerInputAction.GetInputAction();
    }

    public PlayerInputAction GetPlayerInputAction()
    {
        return playerInputAction;
    }

    public PlayerEnemyScan GetPlayerEnemyScan()
    {
        return playerEnemyScan;
    }
}
