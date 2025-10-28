using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;    //���ݷ�
    public int per; //����

    [SerializeField] float bulletSpeed = 15.0f; //�߻� �ӵ�
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        // Remote
        if(per >= 0)
        {
            rb.linearVelocity = dir * bulletSpeed;
        }
    }


    // Use For Remote Weapon
    void OnTriggerEnter(Collider other)
    {
        // Not Enemy OR per is infinite -> return
        if ((!other.CompareTag("Enemy")) || (per == -100))
        {
            return;
        }
        // Found Enemy AND per is not infinite
        else
        {
            // ������� -1 -> �Ѿ� ��Ȱ��ȭ
            per--;
            if (per < 0)
            {
                rb.angularVelocity = Vector3.zero;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Not Enemy OR per is infinite -> return
        if ((!other.CompareTag("Area")) || (per == -100))
        {
            return;
        }
        // Out of Area
        else
        {
            gameObject.SetActive(false);
        }
    }
}
