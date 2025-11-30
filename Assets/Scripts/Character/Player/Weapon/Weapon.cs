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

    Rigidbody playerRb;

    private void Awake()
    {
        playerController = GameManager.instance.playerController;
        weaponLocation = GameManager.instance.weaponLocation;
        playerRb = GameManager.instance.playerController.GetPlayerInputAction().GetRigidbody();
        if(playerRb != null)
        {
            Debug.Log("NOT NULL!");
        }
    }

    // How the Weapon Works?
    private void Update()
    {
        if (GameManager.instance.isGameStop)
        {
            return;
        }

        switch (id)
        {
            //Close Rotating Weapon : Rotate Weapon
            case (int)ItemData.ItemType.Melee:
                transform.Rotate(Vector3.up * speed * Time.deltaTime);
                break;
            //Remote Weapon : Shoot Weapon
            case (int)ItemData.ItemType.Range:
                timer += Time.deltaTime;

                // CoolTime
                if(timer > speed)
                {
                    timer = 0f;
                    Shoot();
                }
                break;
            //Dagger : Shoot Dagger to Forward
            case (int)ItemData.ItemType.Dagger:
                timer += Time.deltaTime;

                // CoolTime
                if (timer > speed)
                {
                    timer = 0f;
                    Shoot_Dagger();
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
        name = "Weapon " + (int)itemData.itemType;
        transform.parent = weaponLocation.transform;
        transform.localPosition = Vector3.zero;

        id = (int)itemData.itemType;
        damage = itemData.baseDamage;
        count = itemData.baseCount;

        switch (id)
        {
            //Close Rotating Weapon
            case (int)ItemData.ItemType.Melee:
                speed = 150;
                Replacement();
                break;
            //Remote Weapon
            case (int)ItemData.ItemType.Range:
                speed = 1.0f;
                break;
            //Dagger
            case (int)ItemData.ItemType.Dagger:
                speed = 1.2f;
                break;
            default:
                break;
        }

        playerController.weaponLocation.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void LevelUp(float damage, int count)
    {
        this.damage =  damage;
        this.count += count;

        //Close Rotating Weapon
        if (id == (int)ItemData.ItemType.Melee)
        {
            Replacement();
        }

        playerController.weaponLocation.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    //Use for CloseWeapon
    private void Replacement()
    {
        float distance = 3.0f;

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

            bullet.localPosition = Vector3.zero + Vector3.down; //down: satellite is up
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
            float dirX = target.position.x - transform.position.x;
            float dirZ = target.position.z - transform.position.z;
            Vector3 direction = new Vector3(dirX, 0, dirZ).normalized;  //(target.position - transform.position).normalized;

            // Set Bullet Location & Rotation
            GameObject bullet = GameManager.instance.poolManager.GetPrefab((int)PoolManager.PoolType.RemoteWeapon);
            if(bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.FromToRotation(Vector3.forward, direction);

                bullet.GetComponent<Bullet>().Init(damage, count, direction);
            }
            
            // (Target is Null)  OR  (Target is Not Active)
            if (target == null || !target.gameObject.activeSelf)
            {
                gameObject.SetActive(false);
                return;
            }
            
        }
    }

    private void Shoot_Dagger()
    {
        Vector3 direction = playerRb.transform.forward;

        GameObject bullet = GameManager.instance.poolManager.GetPrefab((int)PoolManager.PoolType.Dagger);
        if(bullet != null)
        {
            bullet.transform.position = playerRb.transform.position;
            bullet.transform.rotation = Quaternion.LookRotation(direction);

            bullet.GetComponent<Bullet>().Init(damage, count, direction);
        }
    }
}
