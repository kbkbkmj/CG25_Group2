using UnityEngine;

public class AreaLocation : MonoBehaviour
{
    public Transform target;

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position;
        }
    }
}
