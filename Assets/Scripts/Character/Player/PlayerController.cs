using UnityEngine;

public class PlayerController : CharacterController
{
    private PlayerInputAction playerInputAction;

    void Awake()
    {
        playerInputAction = GetComponent<PlayerInputAction>();
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
}
