using UnityEngine;

public class RemoteWeapon : MonoBehaviour
{
    public float speed = 15f;
    public float damage = 5f;
    private Transform target;

    // ... (SetTarget, Update 함수는 그대로) ...
    public void SetTarget(Transform newTarget) { target = newTarget; }

    void Update()
    {
        
        // (Target is Null)  OR  (Target is Not Active)
        if (target == null || !target.gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            return;
        }
        // Calculating Direction & Shoot
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        
    }
}