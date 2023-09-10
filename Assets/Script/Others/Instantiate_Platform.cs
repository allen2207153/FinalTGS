using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Instantiate_Platform : MonoBehaviour
{
    public GameObject Platform;
    public Transform platformPos;
    [SerializeField] float platformSpeed;
    void Start()
    {

    }

    void Update()
    {
      
      
            
    }
    void FixedUpdate()
    {
        Transform transform = this.transform;
        Vector2 pos = transform.position;
        pos.y -= platformSpeed;
        transform.position = pos;

        if (transform.position.y <-202)
        {
            transform.position = platformPos.position;
        }
    }
}
    

   

