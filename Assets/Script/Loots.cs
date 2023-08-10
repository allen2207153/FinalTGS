using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Loots : ScriptableObject
{
    public string lootName;
    public int dropChance;
    public GameObject Item;

    public Loots(string lootName, int dropChance, GameObject Item)
    {
        this.lootName = lootName;
        this.dropChance = dropChance;
        this.Item = Item;
    }
}
