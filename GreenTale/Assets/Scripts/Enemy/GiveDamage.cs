using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDamage : MonoBehaviour
{
    public int damage;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player=FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="Player")
        {
            player.isHurt = true;
            player.currentPlayerHealth -= damage;
        }
    }
    
}
