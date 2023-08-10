using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAttack : MonoBehaviour
{
    GameObject enemyObject;

    float attackTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //don't do anything without target
        if (enemyObject == null) return;

        //found target, what now ?
        if (attackTimer <= 0f) attackTimer = 1f; // wait 1 second before attacking

        //wait for attack
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                attack();
            }
        }
    }
    void attack()
    {
        attackTimer = 0f; // reset cooldown
    }
}
