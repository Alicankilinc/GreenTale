using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJackControl : MonoBehaviour
{
    Rigidbody2D enemyBody2D;
    public float enemySpeed;
    EnemyHealth enemyHealth;

    public bool isGrounded;
    Transform groundCheck;
    const float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public bool moveRight;
    


    //Detect the Edge
    public bool onEdge;
    Transform edgeCheck;

   



    // Start is called before the first frame update
    void Start()
    {
        
        enemyBody2D = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
        edgeCheck = transform.Find("EdgeCheck");
        enemyHealth=GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //Checking if the enemy hits the wall
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        //Checking if the enemy is about to fall
        onEdge = Physics2D.OverlapCircle(edgeCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded || !onEdge)
        {
            moveRight = !moveRight;
        }
        if (moveRight)
        {
            enemyBody2D.velocity = new Vector2(enemySpeed, 0);
            transform.localScale = new Vector2(0.3636f, 0.3636f);
        }
        if (!moveRight)
        {
            enemyBody2D.velocity = new Vector2(-enemySpeed, 0);
            transform.localScale = new Vector2(-0.3636f, 0.3636f);
        }
    }
}
