using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Player player;

    //UI
    public Slider healthBar;
    public Text points;
    
    void Start()
    {
        player=FindObjectOfType<Player>();
        healthBar.maxValue = player.maxPlayerHealth/100f;
    }

    // Update is called once per frame
    void Update()
    {

        points.text="Points:   "+ player.currentPoints.ToString();
        if (player.isDead)
        {
            Invoke("RestartGame", 5);
        }
        UpdateUI();
    }
    public void RestartGame()
    {
        //Scene scene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(scene.name);
        player.RecoverPlayer();
        
        
    }
    void UpdateUI()
    {
        healthBar.value = player.currentPlayerHealth/100f;
        if (player.currentPlayerHealth<1)
        {
            healthBar.minValue = 0;
        }
    }


}
