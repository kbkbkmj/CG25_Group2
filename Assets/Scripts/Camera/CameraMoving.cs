using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 6, -11);

    private void LateUpdate()
    {
        if(target != null)
        {
            transform.position = target.position + offset;
            transform.rotation = Quaternion.Euler(30.0f, 0.0f, 0.0f);
        }
    }
}
