using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player=GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded)
        {
            player.Jump();
            player.canDoubleJump = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !player.isGrounded &&player.canDoubleJump)
        {
            player.DoubleJump();
            player.canDoubleJump = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            player.Shooting();
        }
    }
}
