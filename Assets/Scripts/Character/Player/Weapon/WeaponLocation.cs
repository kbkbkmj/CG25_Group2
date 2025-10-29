using UnityEngine;

public class WeaponLocation : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private void LateUpdate()
    {
        transform.position = playerTransform.position;
    }
}
