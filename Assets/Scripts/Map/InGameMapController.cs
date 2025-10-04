using UnityEngine;

public class InGameMapController : MonoBehaviour
{
    private void OnTriggerExit(Collider collider)
    {
        // If not Area, do NOT Action
        if (!collider.CompareTag("Area")) return;

        // Get Position of Player & Tile Position
        Vector3 playerPosition = GameManager.instance.playerMovement.transform.position;
        Vector3 tilePosition = transform.position;

        // How Long Player Moved from Tile's Centre?
        float diffX = Mathf.Abs(playerPosition.x - tilePosition.x);
        float diffZ = Mathf.Abs(playerPosition.z - tilePosition.z);

        // Get Player Direction by Input Vector
        Vector3 playerDir = GameManager.instance.playerInput.GetInputVector();
        // If x of playerDir is smaller than 0, move Left
        float dirX = (playerDir.x < 0) ? -1 : 1;
        // If z of playerDir is smaller than 0, move backward
        float dirZ = (playerDir.z) < 0 ? -1 : 1;

        switch (transform.tag)
        {
            //Ground Location Change
            case "Ground":
                Debug.Log("DiffX: " + diffX + ", DiffZ: " + diffZ);

                // X move is Larger than Z move -> Move Right or Left
                if (diffX > diffZ)
                {
                    // Move Up(or Down) | Size: two Tile
                    transform.Translate(Vector3.right * dirX * (2 * 100));
                }
                else if (diffX < diffZ)
                {
                    // Size: two Tile
                    transform.Translate(Vector3.forward * dirZ * (2 * 100));
                }
                break;
            case "Enemy":
                break;
        }
    }
}
