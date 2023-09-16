using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peacockAnim : MonoBehaviour
{
    public Animator peacock;
    void Start()
    {
        peacock = GetComponent<BossPeacock>().GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            peacock.enabled = true;

        }
    }
}
