using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 15f;
    public float damage = 5f;
    private Transform target;

    // ... (SetTarget, Update 함수는 그대로) ...
    public void SetTarget(Transform newTarget) { target = newTarget; }
    void Update()
    {
        if (target == null || !target.gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            return;
        }
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }


    // ▼▼▼▼▼ 이 함수 내부를 수정하세요! (GetComponent -> GetComponentInParent) ▼▼▼▼▼
    void OnTriggerEnter(Collider other)
    {
        // 충돌한 물체(other)가 'Enemy' 태그를 가졌는지 '부모'까지 거슬러 올라가서 확인합니다.
        if (other.CompareTag("Enemy") || other.transform.parent.CompareTag("Enemy"))
        {
            // 데미지를 주기 위해 '부모'에게 붙어있는 EnemyController를 찾습니다.
            EnemyController ec = other.GetComponentInParent<EnemyController>();

            if (ec != null)
            {
                ec.TakeDamage(damage);
            }

            // 총알 자신은 비활성화됩니다.
            gameObject.SetActive(false);
        }
    }
    // ▲▲▲▲▲ 여기까지 수정 ▲▲▲▲▲
}