using UnityEngine;

public class InGameMapController : MonoBehaviour
{
    [SerializeField] private float tileSize = 100.0f;
    private Collider enemyCollider;

    private void Awake()
    {
        enemyCollider = GetComponent<Collider>();
    }

    private void OnTriggerExit(Collider collider)
    {
        // If not Area, do NOT Action
        if (!collider.CompareTag("Area")) return;

        // Get Position of Player & Tile Position
        Vector3 playerPosition = GameManager.instance.playerController.transform.position;
        Vector3 tilePosition = transform.position;

        // Where Player Moved from Tile's Centre?
        float dirX = playerPosition.x - tilePosition.x;     // Lower than 0: Left , Larger than 0: Right
        float dirZ = playerPosition.z - tilePosition.z;     // Lower than 0: Up , Larger than 0: Down

        // How Long the Player Moved from Tile's Centre?
        float diffX = Mathf.Abs(dirX);
        float diffZ = Mathf.Abs(dirZ);

        //Select Direction
        dirX = (dirX > 0) ? 1 : -1;     // Larger than 0: Right, Lower than 0: Left
        dirZ = (dirZ > 0) ? 1 : -1;     // Larger than 0: Down, Lower than 0: Up

        switch (transform.tag)
        {
            //Ground Location Change
            case "Ground":
                //Debug.Log("DiffX: " + diffX + ", DiffZ: " + diffZ);
                //Debug.Log($"GROUND - diffX: {diffX}, diffZ: {diffZ}, tileSize: {tileSize}");


                // X move is Larger than Tile Size -> Move Right or Left
                if (diffX > tileSize)
                {
                    // Move Up(or Down) | Size: two Tile
                    transform.Translate(Vector3.right * dirX * (tileSize * 2));
                }
                // Z move is Larger than Tile Size -> Move Up or Down
                if (diffZ > tileSize)
                {
                    // Size: two Tile
                    transform.Translate(Vector3.forward * dirZ * (tileSize * 2));
                }
                    break;
            case "Enemy":
                if (enemyCollider.enabled)
                {
                    Debug.Log("EXIT!!");
                    Debug.Log($"ENEMY - diffX: {diffX}, diffZ: {diffZ}, tileSize: {tileSize}, dirX: {dirX}, dirZ: {dirZ}");


                    // X move is Larger than Tile Size -> Move Right or Left
                    if (diffX > tileSize / 2)
                    {
                        // Move Up(or Down) | Size: two Tile
                        transform.Translate(Vector3.right * dirX * tileSize, Space.World);
                    }
                    // Z move is Larger than Tile Size -> Move Up or Down
                    if (diffZ > tileSize / 2)
                    {
                        // Size: two Tile
                        transform.Translate(Vector3.forward * dirZ * tileSize, Space.World);
                    }
                }
                break;
        }
    }
}
