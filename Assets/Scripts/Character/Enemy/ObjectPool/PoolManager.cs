using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //Variables for Saving Prefabs
    public GameObject[] prefabs;
    //Lists which act like Pool - 1 Pool for 1 Variable
    public List<GameObject>[] pools;
    //Max Pool Size
    public int[] poolSizes;
    //Is Any Spawnable Objects
    public bool[] isSpawnable;

    public enum PoolType { Enemy, ExperienceGem, PlayerCloseWeapon, RemoteWeapon }

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

        // Not All Spawned yet -> Instantiate
        if (pools[index].Count < poolSizes[index])
        {
            // Not Found -> Make New Object & Set as a Child
            if (selected == null)
            {
                selected = Instantiate(prefabs[index], transform);
                pools[index].Add(selected);     //Register to the Pool

                if (pools[index].Count >= poolSizes[index])
                {
                    isSpawnable[index] = false;
                }
            }
        }
        // All Spawned -> Select from Pool
        else
        {
            // Find Disabled Object
            foreach (GameObject item in pools[index])
            {
                // Found "Not Active" -> Select!
                if (!item.activeSelf)
                {
                    selected = item;
                    selected.SetActive(true);

                    // Count how many avaliable objects (if zero, cannot use pool)
                    int activeCount = pools[index].Count(obj => !obj.activeSelf);
                    if(activeCount == 0)
                    {
                        isSpawnable[index] = false;
                    }
                    break;
                }
            }
        }

        return selected;
    }

}
