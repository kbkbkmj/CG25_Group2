using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "Scriptable Objects/PlayerStatus")]
public class PlayerStatus : ScriptableObject
{
    //�ִ� ü��
    [SerializeField] private float hp = 100.0f;
    public float HP { get { return hp; } }

    //ȸ��
    [SerializeField] private float recovery = 0.0f;
    public float Recovery { get { return recovery; } }

    //����
    [SerializeField] private float armor = 0.0f;
    public float Armor { get { return armor; } }

    //�̵��ӵ�
    [SerializeField] private float moveSpeed = 0.0f;
    public float MoveSpeed { get { return moveSpeed; } }
}
