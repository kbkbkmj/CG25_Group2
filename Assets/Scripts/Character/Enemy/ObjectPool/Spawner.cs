using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Location & Data
    public Transform[] spawnPoint;
    public EnemyStatus[] spawnData;

    private int level;
    private float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Time Update
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10.0f), spawnData.Length - 1);

        // Spawn Event
        if (timer > spawnData[level].SpawnTime) {
            timer = 0.0f;
            Spawn();
        }
    }

    // Enemy Spawn
    private void Spawn()
    {
        bool isSpawnable = GameManager.instance.poolManager.elements[(int)PoolManager.PoolType.Enemy].IsSpawnable;

        if (isSpawnable)
        {
            GameObject enemy = GameManager.instance.poolManager.GetPrefab((int)PoolManager.PoolType.Enemy);   //Get From Pool
            enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;     //0: Parent, 1~: Children
            enemy.GetComponent<EnemyController>().Init(spawnData[level]);   // Set Enemy Data
            //Debug.Log("Spawned");
        }
    }
}

/*
[System.Serializable]
public class SpawnData
{
    public int modelType;
    public float spawnTime;
    public int hp;
    public float speed;
}
*/