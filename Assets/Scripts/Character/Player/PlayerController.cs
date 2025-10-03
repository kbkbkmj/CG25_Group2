using UnityEngine;

public class PlayerController : CharacterController
{
    private PlayerInputAction playerInputAction;

    void Awake()
    {
        playerInputAction = GetComponent<PlayerInputAction>();
    }

    void FixedUpdate()
    {
        playerInputAction.GetInputAction();
    }
}
