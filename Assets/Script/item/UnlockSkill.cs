using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSkill : MonoBehaviour
{
    public GameObject ItemDescription;
    public GameObject dropHitBox;
    GameObject player;
    bool sceneOpen;
    [SerializeField] private AudioSource itemEffect;
    void Start()
    {
         player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack") && sceneOpen == true)
        {
            Close();
            sceneOpen = false;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if ( other.gameObject.CompareTag("Player") &&other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            
            dropHitBox.GetComponent<DropAttack>().enabled = true;
            itemEffect.Play();
            ItemDescription.SetActive(true);
            player.GetComponent<PlayerController>().FreezeInput(true);
            player.GetComponent<PlayerController>().FreezePlayer(true);
           sceneOpen = true;
        }
    }
    void Close()
    {
        ItemDescription.SetActive(false);
        player.GetComponent<PlayerController>().FreezeInput(false);
        player.GetComponent<PlayerController>().FreezePlayer(false);
        Destroy(gameObject);
    }
}
