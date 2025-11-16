using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);

    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.GameStop();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.GameResume();
    }

    public void Select(int i)
    {
        items[i].OnClick();
    }

    public void Next()
    {
        // Make All Items Unabled
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        // Select 3 Random Items
        int[] rand = new int[3];

        while (true)
        {
            rand[0] = Random.Range(0, items.Length);
            rand[1] = Random.Range(0, items.Length);
            rand[2] = Random.Range(0, items.Length);

            if ((rand[0] != rand[1]) && (rand[1] != rand[2]) && (rand[0] != rand[2]))
            {
                break;
            }
        }

        // Activate 3 items
        for(int i = 0; i< rand.Length; i++)
        {
            Item randItem = items[rand[i]];
            
            // If Full Level, turn into HP
            if(randItem.level == randItem.itemData.damages.Length)
            {
                items[4].gameObject.SetActive(true);
            }
            // Else, Activate
            else
            {
                randItem.gameObject.SetActive(true);
            }
        }



    }
}
