using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 15f;
    public float damage = 5f;
    private Transform target;

    // ... (SetTarget, Update �Լ��� �״��) ...
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


    // ������ �� �Լ� ���θ� �����ϼ���! (GetComponent -> GetComponentInParent) ������
    void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü(other)�� 'Enemy' �±׸� �������� '�θ�'���� �Ž��� �ö󰡼� Ȯ���մϴ�.
        if (other.CompareTag("Enemy") || other.transform.parent.CompareTag("Enemy"))
        {
            // �������� �ֱ� ���� '�θ�'���� �پ��ִ� EnemyController�� ã���ϴ�.
            EnemyController ec = other.GetComponentInParent<EnemyController>();

            if (ec != null)
            {
                ec.TakeDamage(damage);
            }

            // �Ѿ� �ڽ��� ��Ȱ��ȭ�˴ϴ�.
            gameObject.SetActive(false);
        }
    }
    // ������ ������� ���� ������
}