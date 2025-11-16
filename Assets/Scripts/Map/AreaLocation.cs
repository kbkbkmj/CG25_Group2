using UnityEngine;

public class AreaLocation : MonoBehaviour
{
    public Transform target;

    private void LateUpdate()
    {
        if (GameManager.instance.isGameStop)
        {
            return;
        }

        if (target != null)
        {
            transform.position = target.position;
        }
    }
}
