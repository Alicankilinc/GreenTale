using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCam : MonoBehaviour
{
    Player pplayerr;
    public Transform player;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
     pplayerr=FindObjectOfType<Player>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x<-10 && transform.position.y<-10)
        {

            transform.position = transform.position;

        }
        else if(pplayerr.isDead)
        {
            transform.position = transform.position;
            Invoke("FollowPlayer", 5);
        }
        else
        {
            FollowPlayer();
        }
    }
    void FollowPlayer()
    {
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);
    }
 

}
