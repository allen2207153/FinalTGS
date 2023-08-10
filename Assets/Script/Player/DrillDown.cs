using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillDown : MonoBehaviour
{
    private Animator anim;
    public float time;
    private Rigidbody2D rb;
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {


    }

    
  
}
