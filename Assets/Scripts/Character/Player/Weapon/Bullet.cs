using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;    //공격력
    public int per; //관통

    [SerializeField] float bulletSpeed = 15.0f; //발사 속도
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
            // 관통력이 -1 -> 총알 비활성화
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
