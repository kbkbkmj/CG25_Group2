using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using static UnityEngine.GraphicsBuffer;

public class Weapon : MonoBehaviour
{
    public int id;          // Weapon Type
    public float damage;    // Damage of Weapon
    public int count;       // How Many Weapons
    public float speed;     // How Fast || 근접: 회전 속도, 원거리: 발사 쿨타임

    private float timer = 0f;    // For Remote
    PlayerController playerController;
    WeaponLocation weaponLocation;

    private void Awake()
    {
        playerController = GameManager.instance.playerController;
        weaponLocation = GameManager.instance.weaponLocation;
    }

    // How the Weapon Works?
    private void Update()
    {
        switch (id)
        {
            //Close Rotating Weapon : Rotate Weapon
            case 0:
                transform.Rotate(Vector3.up * speed * Time.deltaTime);
                break;
            //Remote Weapon : Shoot Weapon
            case 1:
                timer += Time.deltaTime;

                // CoolTime
                if(timer > speed)
                {
                    timer = 0f;
                    Shoot();
                }
                break;
            default:
                break;
        }

        //TEST
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LevelUp(20, 1);
        }
        */
    }

    //Setting
    public void Init(ItemData itemData)
    {
        //Basic Set
        name = "Weapon " + itemData.itemId;
        transform.parent = weaponLocation.transform;
        transform.localPosition = Vector3.zero;

        id = itemData.itemId;
        damage = itemData.baseDamage;
        count = itemData.baseCount;

        switch (id)
        {
            //Close Rotating Weapon
            case 0:
                speed = 150;
                Replacement();
                break;
            //Remote Weapon
            case 1:
                speed = 1.0f;
                break;
            default:
                break;
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage =  damage;
        this.count += count;

        //Close Rotating Weapon
        if (id == 0)
        {
            Replacement();
        }
    }

    //Use for CloseWeapon
    private void Replacement()
    {
        float distance = 1.0f;

        for(int i = 0; i < count; i++)
        {
            Transform bullet;
            // Use existing Ones
            if(i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            // If insufficient, Get from the pool
            else
            {
                bullet = GameManager.instance.poolManager.GetPrefab((int)PoolManager.PoolType.PlayerCloseWeapon).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;
            
            //Rotate Weapon & Replace
            Vector3 rotation = (Vector3.up * 360 * i) / count;
            bullet.Rotate(rotation);
            bullet.Translate(bullet.forward * distance, Space.World);

            //Set Damage & Per.
            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero); // -100 : Infinite Per.
        }
    }

    //Use for Remote Weapon
    private void Shoot()
    {
        // Found Closest Enemy
        if ((playerController.GetPlayerEnemyScan().closestEnemy != null))
        {
            // Set Target
            Transform target = playerController.GetPlayerEnemyScan().closestEnemy;

            // Calculating Direction
            Vector3 direction = (target.position - transform.position).normalized;  

            // Set Bullet Location & Rotation
            GameObject bullet = GameManager.instance.poolManager.GetPrefab((int)PoolManager.PoolType.RemoteWeapon);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.FromToRotation(Vector3.forward, direction);

            bullet.GetComponent<Bullet>().Init(damage, count, direction);

            
            // (Target is Null)  OR  (Target is Not Active)
            if (target == null || !target.gameObject.activeSelf)
            {
                gameObject.SetActive(false);
                return;
            }
            
        }
    }
}
