using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Instantiate_Platform : MonoBehaviour
{
    public GameObject Platform;
    public Transform platformPos;
    
    void Start()
    {

    }

    void Update()
    {
      
       Transform transform = this.transform;
       Vector2 pos = transform.position;
        pos.y -= 0.02f;
        transform.position = pos;
        if (transform.position.y < -205)
        {
            transform.position = platformPos.position;
        }
            
    }
    
}
    

   

