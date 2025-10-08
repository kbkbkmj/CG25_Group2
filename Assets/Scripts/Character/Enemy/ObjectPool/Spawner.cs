using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
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

        if (timer > 0.5f) {
            timer = 0.0f;
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject enemy = GameManager.instance.poolManager.GetPrefab(Random.Range(0, 2));
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;     //0: Parent, 1~: Children
    }
}
