using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private PlayerController playerController;
    public GameObject player;
    public Transform respawnPoint;
    public int damage;
    public  GameObject TrapScene;
    // Start is called before the first frame update
    void Start()
    {
             playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
             playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") &&other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            playerHealth.DamagePlayer(damage);
            player.GetComponent<PlayerController>().FreezeInput(true);
            player.GetComponent<PlayerController>().FreezePlayer(true);
            player.transform.position = respawnPoint.position;
            TrapScene.SetActive(true);
            player.GetComponent<PlayerController>().FreezePlayer(false);
            Close();
        }
    }

    void Close()
    {
        StartCoroutine(CloseScene());
    }
    IEnumerator CloseScene()
    {
        yield return new WaitForSeconds(1f);
        TrapScene.SetActive(false);
        player.GetComponent<PlayerController>().FreezeInput(false);
        
    }
}
