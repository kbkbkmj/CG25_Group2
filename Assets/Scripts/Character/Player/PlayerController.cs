using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputAction playerInputAction;
    private PlayerEnemyScan playerEnemyScan;
    public GameObject weaponLocation;
    public PlayerAnim playerAnim;

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

    private void OnCollisionStay(Collision other)
    {
        if (GameManager.instance.isGameStop)
        {
            return;
        }
        

        if (other.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.hp -= 10 * Time.deltaTime;

            if (GameManager.instance.hp < 0)
            {
                playerAnim.Dead();
                GameManager.instance.playerController.gameObject.GetComponent<Collider>().enabled = false;
                GameManager.instance.playerController.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                GameManager.instance.GameOver();
            }
        }

        
    }
}
