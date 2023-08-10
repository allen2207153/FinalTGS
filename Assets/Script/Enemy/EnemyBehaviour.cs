using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : Enemy
{
    Rigidbody2D rigidbody;
    public int moveSpeed;
    public float moveDir;
    int direction = 1;
    float initx, inity;
    Vector2 position;
    float currentDis;
    void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody2D>();
        initx = transform.position.x;
        inity = transform.position.y;
    }

    // Update is called once per frame
    public void Update()
    {
        base.Update();
    }
        private void FixedUpdate()
    {
        position = rigidbody.position;
            position.x += moveSpeed * direction * Time.deltaTime;
            currentDis = Mathf.Abs(position.x - initx);
            if(currentDis < 0.01f || currentDis >moveDir)
            {
                direction *= -1;
            }
        rigidbody.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        direction *= -1;

    }
}
