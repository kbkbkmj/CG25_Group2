using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData itemData)
    {
        //Basic Set
        name = "Gear" + itemData.itemId;
        transform.parent = GameManager.instance.playerController.weaponLocation.transform;
        transform.localPosition = Vector3.zero;

        //Property Set
        type = itemData.itemType;
        rate = itemData.damages[0];
        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    public void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    //Weapon Speed Rate Up
    public void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons)
        {
            switch (weapon.id)
            {
                //Close Weapon
                case 0:
                    weapon.speed = 150 + (150 * rate);
                    break;
                //Remote Weapon
                case 1:
                    weapon.speed = 0.5f * (1.0f - rate);
                    break;
                default:
                    break;
            }
        }
    }

    public void SpeedUp()
    {
        float speed = 3;
        speed = speed + speed * rate;
        GameManager.instance.playerController.GetPlayerInputAction().GetPlayerMovement().SetSpeed(speed);
    }
}
