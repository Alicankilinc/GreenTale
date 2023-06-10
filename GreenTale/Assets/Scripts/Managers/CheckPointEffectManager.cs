using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointEffectManager : MonoBehaviour
{
    public GameObject idle, playerIn;

    
    private void Start()
    {
        playerIn.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="Player")
        {
            idle.SetActive(false);
            playerIn.SetActive(true);
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            idle.SetActive(true);
            playerIn.SetActive(false);
        }
    }


}
