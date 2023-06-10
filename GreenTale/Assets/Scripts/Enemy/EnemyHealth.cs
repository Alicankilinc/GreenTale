using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxEnemyHealth=100;
    public float currentEnemyHealth;
    public bool gotDamage;
    public float damage;
    public float shootingDamage = 25;


    CircleCollider2D cir2D;
    Rigidbody2D body2D;
    Player player;
    Animator enemyJackAnim;

    //Audio
    AudioSource au_Source;
    AudioClip ac_Dead;

    // Start is called before the first frame update
    void Start()
    {
        enemyJackAnim = GetComponent<Animator>();
        currentEnemyHealth = maxEnemyHealth;
        player = FindObjectOfType<Player>();
        cir2D=GetComponent<CircleCollider2D>();
        body2D = GetComponent<Rigidbody2D>();
        au_Source=GetComponent<AudioSource>();
        ac_Dead = Resources.Load("SoundEffects/EnemyDead") as AudioClip;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnemyHealth < 1)
        {
            enemyJackAnim.SetBool("isDead", true);
            cir2D.enabled = false;
            
            Destroy(gameObject, 1);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerItem" && player.canDamage)
        {
            currentEnemyHealth -= damage;
            au_Source.PlayOneShot(ac_Dead);
        }
        if (other.gameObject.tag == "PlayerShooting")
        {
            currentEnemyHealth -= shootingDamage;
            au_Source.PlayOneShot(ac_Dead);
            Destroy(other.gameObject);
        }
    }
}
