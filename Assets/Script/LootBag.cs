using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<Loots> lootList = new List<Loots>();
    Loots GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);
        List<Loots> possibleItem = new List<Loots>();   
        foreach(Loots item in lootList)
        {
            if(randomNumber <= item.dropChance) 
            {
                possibleItem.Add(item);
            }
        }
        if(possibleItem.Count > 0)
        {
            Loots droppedItem = possibleItem[Random.Range(0,possibleItem.Count)];
            return droppedItem;
        }
        Debug.Log("No loot dropped");
        return null;    
    }
    public void InstantiateLoot(Vector2 spawnPosition)
    {
        Loots droppedItem = GetDroppedItem();
        if(droppedItem != null) 
        {
            GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
            

            float dropForce = 300f;
            Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
    }
}
