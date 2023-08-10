using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laternDestroy : MonoBehaviour
{
    public float startTime = 0.01f;
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("HitBox") && other.GetType().ToString() == "UnityEngine.PolygonCollider2D")
        {
            GetComponent<itemDrop>().DropItem();
            StartCoroutine(DestroyObject());

        }
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(startTime);
        Destroy(gameObject);
    }
}
