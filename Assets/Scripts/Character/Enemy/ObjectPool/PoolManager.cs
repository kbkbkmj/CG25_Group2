using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //Variables for Saving Prefabs
    public GameObject[] prefabs;
    //Lists which act like Pool - 1 Pool for 1 Variable
    public List<GameObject>[] pools;

    void Awake()
    {
        // Create Pools
        pools = new List<GameObject>[prefabs.Length];

        // Reset Pools
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject GetPrefab(int index)
    {
        GameObject selected = null;

        // Find Disabled Object
        foreach (GameObject item in pools[index]) {
            // Found "Not Active" -> Select!
            if (!item.activeSelf)
            {
                selected = item;
                selected.SetActive(true);
                break;
            }
        }

        // Not Found -> Make New Object & Set as a Child
        if (selected == null) {
            selected = Instantiate(prefabs[index], transform);
            pools[index].Add(selected);     //Register to the Pool
        }

        return selected;
    }

}
