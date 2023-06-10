using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    Rigidbody2D body2D;
    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        body2D = GetComponent<Rigidbody2D>();
        body2D.AddForce(new Vector2(bulletSpeed,0),ForceMode2D.Impulse);
        Invoke("SelfDestroy", 3);
    }

   
 
    void SelfDestroy()

    {
        Destroy(gameObject);
    }
   
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Ground")
        {
            Destroy(gameObject);
        }
    }

}
