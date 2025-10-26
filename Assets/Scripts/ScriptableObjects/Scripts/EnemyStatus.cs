using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatus", menuName = "Scriptable Objects/EnemyStatus")]
public class EnemyStatus : ScriptableObject
{
    [SerializeField] private Mesh mesh;
    public Mesh GetMesh {  get { return mesh; } }

    [SerializeField] private Material material;
    public Material GetMaterial { get { return material; } }



    [SerializeField] private float spawnTime = 0.0f;
    public float SpawnTime { get { return spawnTime; } }

    [SerializeField] private int modelingType = 0;
    public int ModelingType { get { return modelingType; } }

    [SerializeField] private float maxHp = 10.0f;
    public float MaxHp { get { return maxHp; } }

    [SerializeField] private float speed = 3.0f;
    public float Speed { get { return speed; } }
}
