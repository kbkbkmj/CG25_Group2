using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;    //공격력
    public int per; //관통

    public void Init(float damage, int per)
    {
        this.damage = damage;
        this.per = per;
    }
}
