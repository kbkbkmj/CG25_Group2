using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using static UnityEngine.GraphicsBuffer;

public class Weapon : MonoBehaviour
{
    public int id;
    public float damage;
    public int count;
    public float speed;

    private float timer = 0f;
    PlayerController playerController;
    WeaponLocation weaponLocation;
    private Rigidbody playerRb;

    private void Start()
    {
        // 1. GameManager 연결
        if (GameManager.instance == null) return;

        playerController = GameManager.instance.playerController;
        weaponLocation = GameManager.instance.weaponLocation;

        // 2. 플레이어 Rigidbody 찾기 (탐정 코드 포함)
        if (playerController != null)
        {
            var inputAction = playerController.GetPlayerInputAction();
            if (inputAction != null)
            {
                playerRb = inputAction.GetRigidbody();
            }
        }
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.up * speed * Time.deltaTime);
                break;
            case 1:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Shoot_Homing();
                }
                break;
            case 2:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Shoot_Forward();
                }
                break;
            default:
                break;
        }
    }

    public void Init(ItemData itemData)
    {
        // ▼▼▼▼▼ [오류 수정 핵심] Init이 Start보다 먼저 실행될 경우를 대비한 안전장치 ▼▼▼▼▼
        if (weaponLocation == null)
        {
            // weaponLocation이 비어있다면 GameManager에서 즉시 가져옵니다.
            weaponLocation = GameManager.instance.weaponLocation;
        }
        // ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

        name = "Weapon " + itemData.itemId;

        // 이제 weaponLocation이 확실히 있으므로 오류가 나지 않습니다.
        transform.parent = weaponLocation.transform;
        transform.localPosition = Vector3.zero;

        id = itemData.itemId;
        damage = itemData.baseDamage;
        count = itemData.baseCount;

        switch (id)
        {
            case 0:
                speed = 150;
                Replacement();
                break;
            case 1:
                speed = 1.0f;
                break;
            case 2:
                damage = 2;
                count = 0;
                speed = 1.2f;
                break;
            default:
                break;
        }
    }

    public void LevelUp(float damage, int count)
    {
        if (id == 2)
        {
            this.damage *= 2;
            this.count += 1;
        }
        else
        {
            this.damage = damage;
            this.count += count;
        }

        if (id == 0)
        {
            Replacement();
        }
    }

    private void Replacement()
    {
        float distance = 1.0f;
        for (int i = 0; i < count; i++)
        {
            Transform bullet;
            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.instance.poolManager.GetPrefab((int)PoolManager.PoolType.PlayerCloseWeapon).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotation = (Vector3.up * 360 * i) / count;
            bullet.Rotate(rotation);
            bullet.Translate(bullet.forward * distance, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero);
        }
    }

    private void Shoot_Homing()
    {
        if ((playerController.GetPlayerEnemyScan().closestEnemy != null))
        {
            Transform target = playerController.GetPlayerEnemyScan().closestEnemy;
            Vector3 direction = (target.position - transform.position).normalized;

            GameObject bullet = GameManager.instance.poolManager.GetPrefab((int)PoolManager.PoolType.RemoteWeapon);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.FromToRotation(Vector3.forward, direction);

            bullet.GetComponent<Bullet>().Init(damage, count, direction);

            if (target == null || !target.gameObject.activeSelf)
            {
                gameObject.SetActive(false);
                return;
            }
        }
    }

    private void Shoot_Forward()
    {
        if (playerRb == null) return;

        Vector3 direction = playerRb.transform.forward;

        GameObject bullet = GameManager.instance.poolManager.GetPrefab((int)PoolManager.PoolType.Dagger);

        bullet.transform.position = playerRb.transform.position;
        bullet.transform.rotation = Quaternion.LookRotation(direction);

        bullet.GetComponent<Bullet>().Init(damage, count, direction);
    }
}