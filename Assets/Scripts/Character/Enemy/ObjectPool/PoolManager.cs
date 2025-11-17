using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public PoolElement[] elements;
    
    //Lists which act like Pool - 1 Pool for 1 Variable
    public List<GameObject>[] pools;

    //Variables for Saving Prefabs
    //public GameObject[] prefabs;
    
    //Max Pool Size
    //public int[] poolSizes;
    //Is Any Spawnable Objects
    //public bool[] isSpawnable;

    public enum PoolType { Enemy, ExperienceGem, PlayerCloseWeapon, RemoteWeapon, Dagger }

    void Awake()
    {
        // Create Pools
        pools = new List<GameObject>[elements.Length];

        // Reset Pools
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
            elements[i].IsSpawnable = true;
        }
    }

    public GameObject GetPrefab(int index)
    {
        GameObject selected = null;

        // Not All Spawned yet -> Instantiate
        if (pools[index].Count < elements[index].PoolSize)
        {
            // Not Found -> Make New Object & Set as a Child
            if (selected == null)
            {
                selected = Instantiate(elements[index].Prefab, transform);
                pools[index].Add(selected);     //Register to the Pool

                if (pools[index].Count >= elements[index].PoolSize)
                {
                    elements[index].IsSpawnable = false;
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
                        elements[index].IsSpawnable = false;
                    }
                    break;
                }
            }
        }

        return selected;
    }

}
