using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_PlayerCube : MonoBehaviour
{
    //Assign enemy mesh renderer
    public MeshRenderer enemy;
    public Text gameOverText;

    Transform thisT;
    MeshRenderer mr;

    //Global static variable
    public static bool GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        thisT = transform;
        mr = GetComponent<MeshRenderer>();
        gameOverText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver)
            return;

        if (gameOverText.enabled)
        {
            //Game has resumed, disable game over text
            gameOverText.enabled = false;
        }

        //Scale player cube with mouse movement
        Vector3 playerScale = (new Vector3(Screen.width / 2 - Input.mousePosition.x, 1, Screen.height / 2 - Input.mousePosition.y)).normalized * 10;
        //Keep Y scale at 10
        playerScale.y = 10;
        //Limit minimum X and Z scale to 0.1
        if (playerScale.x >= 0 && playerScale.x < 0.1f)
        {
            playerScale.x = 0.1f;
        }
        else if (playerScale.x < 0 && playerScale.x > -0.1f)
        {
            playerScale.x = -0.1f;
        }
        if (playerScale.z >= 0 && playerScale.z < 0.1f)
        {
            playerScale.z = 0.1f;
        }
        else if (playerScale.z < 0 && playerScale.z > -0.1f)
        {
            playerScale.z = -0.1f;
        }
        thisT.localScale = playerScale;

        //Check if enemy have intersected with the player, if so, stop the game
        if (mr.bounds.Intersects(enemy.bounds))
        {
            GameOver = true;
            gameOverText.enabled = true;
        }
    }
}