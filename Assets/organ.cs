using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class organ : MonoBehaviour
{
    public GameObject timeline;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            timeline.SetActive(true);
        }
    }
}
