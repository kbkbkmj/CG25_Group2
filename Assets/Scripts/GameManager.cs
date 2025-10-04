using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController playerController;
    public PlayerInputAction playerInput;
    public PlayerMovement playerMovement;

    private void Awake()
    {
        instance = this;
    }
}
