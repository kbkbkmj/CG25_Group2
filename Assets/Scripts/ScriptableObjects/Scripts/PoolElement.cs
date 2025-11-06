using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "PoolElement", menuName = "Scriptable Objects/PoolElement")]
public class PoolElement : ScriptableObject
{
    //Variables for Saving Prefabs
    [SerializeField] private GameObject prefab;
    //Max Pool Size
    [SerializeField] private int poolSize;
    //Is Any Spawnable Objects
    [SerializeField] private bool isSpawnable = true;

    public GameObject Prefab { get { return prefab; } }

    public int PoolSize { get { return poolSize; } }

    public bool IsSpawnable {  
        get { return isSpawnable;}
        set { isSpawnable = value;}
    }
}
