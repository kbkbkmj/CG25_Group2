using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;    //���ݷ�
    public int per; //����

    public void Init(float damage, int per)
    {
        this.damage = damage;
        this.per = per;
    }
}
