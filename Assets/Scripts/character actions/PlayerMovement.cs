using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    int xPos, yPos;
    bool canMove, keepMoving;
    Game gameManager;
    MapManager mapManager;

    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);

        GameObject gameGO = GameObject.Find("GameManager");
        gameManager = gameGO.GetComponent<Game>();
        mapManager = gameGO.GetComponent<MapManager>();

        canMove = true;
        keepMoving = false;
        xPos = 0;
        yPos = 4;
    }

    void Update()
    {
        if (canMove)
        {
            if (Input.GetAxisRaw("Vertical") == 1)
            {
                if (mapManager.map[xPos, yPos].w == 1)
                {
                    yPos++;
                    canMove = false;
                }
            }
            if (Input.GetAxisRaw("Vertical") == -1)
            {
                if (mapManager.map[xPos, yPos].y == 4)
                {
                    yPos--;
                    canMove = false;
                }
            }
            if (Input.GetAxisRaw("Horizontal") == -1)
            {
                if (mapManager.map[xPos, yPos].z == 8)
                {
                    xPos--;
                    canMove = false;
                }
            }
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                if (mapManager.map[xPos, yPos].x == 2)
                {
                    xPos++;
                    canMove = false;
                }
            }
            this.gameObject.transform.position = new Vector3Int(xPos -2, yPos - 2, 0);
            mapManager.visibility[xPos, yPos] = 1;
            mapManager.DrawMap();
            if (mapManager.roomTypes[xPos, yPos] == 0)
            {
                keepMoving = true;
            }
            else if (mapManager.roomTypes[xPos, yPos] == 1)
            {
                SceneManager.LoadScene("Battle");
            }
        }
        //wait for key up
        if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0 && keepMoving)
        {
            canMove = true;
            keepMoving = false;
        }
    }



}
