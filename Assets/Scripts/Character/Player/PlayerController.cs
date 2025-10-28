using UnityEngine;

public class PlayerController : CharacterController
{
    private PlayerInputAction playerInputAction;
    private PlayerEnemyScan playerEnemyScan;

    void Awake()
    {
        playerInputAction = GetComponent<PlayerInputAction>();
        playerEnemyScan = GetComponentInChildren<PlayerEnemyScan>();
    }

    //Physics -> FixedUpdate
    void FixedUpdate()
    {
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
