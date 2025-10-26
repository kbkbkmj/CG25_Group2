using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "Scriptable Objects/PlayerStatus")]
public class PlayerStatus : ScriptableObject
{
    //최대 체력
    [SerializeField] private float hp = 100.0f;
    public float HP { get { return hp; } }

    //회복
    [SerializeField] private float recovery = 0.0f;
    public float Recovery { get { return recovery; } }

    //방어력
    [SerializeField] private float armor = 0.0f;
    public float Armor { get { return armor; } }

    //이동속도
    [SerializeField] private float moveSpeed = 0.0f;
    public float MoveSpeed { get { return moveSpeed; } }
}
