using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEst : MonoBehaviour
{
   public List<GameObject> wayPoints;
    public float speed = 2.0f;
    int index = 0;
    private void Update()
    {
        Vector2 destination = wayPoints[index].transform.position;
        Vector2 newPos = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        transform.position = newPos;

        float distance = Vector2.Distance(transform.position, destination);
        if(distance <=0.05)
        {
            if(index < wayPoints.Count-1)
            {
                index++;
            }
          
        }
    }
}
