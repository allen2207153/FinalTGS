using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCollider : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("wwwwww");
        if (other.CompareTag("Trap") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            Destroy(other.gameObject);



        }
    }
}
