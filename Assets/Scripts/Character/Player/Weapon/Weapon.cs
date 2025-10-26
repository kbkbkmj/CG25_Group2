using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }

    public void Init()
    {
        switch (id)
        {
            //Close Rotating Weapon
            case 0:
                speed = -150;
                Replacement();
                break;
            default:
                break;
        }
    }

    private void Replacement()
    {
        for(int i = 0; i < count; i++)
        {
            Transform bullet =  GameManager.instance.poolManager.GetPrefab(prefabId).transform;
            bullet.parent = transform;
            bullet.GetComponent<Bullet>().Init(damage, -1); // -1 : Infinite Per.
        }

    }
}
