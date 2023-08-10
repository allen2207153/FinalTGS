using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDrop : MonoBehaviour
{
    public GameObject item;
    //private void OnEnable()
    //{
    //    Enemy.OnEnemyDie += DropItem;
    //}
    public void DropItem()
    {
        Instantiate(item, transform.position, Quaternion.identity);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
