using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDestory : MonoBehaviour
{
    private Animator anim;
    [SerializeField] float startTime = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("HitBox") && other.GetType().ToString() == "UnityEngine.PolygonCollider2D")
        {

            StartCoroutine(StartDestroy());
        }
    }
    IEnumerator StartDestroy()
    {
        yield return new WaitForSeconds(startTime);
        anim.SetTrigger("Destroy");

    }
    void destroyRock()
    {
        
        Destroy(gameObject);
    }
    }
  
