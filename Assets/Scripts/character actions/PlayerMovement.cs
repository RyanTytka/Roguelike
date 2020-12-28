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
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        gameObject.SetActive(false);

        GameObject gameGO = GameObject.Find("GameManager");
        gameManager = gameGO.GetComponent<Game>();
        mapManager = gameGO.GetComponent<MapManager>();

        canMove = true;
        keepMoving = false;
        xPos = 0;
        yPos = 4;
    }
    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameObject.SetActive(scene.name == "Map");

        if (scene.name == "Map")
        {
            canMove = true;
            keepMoving = false;
        }
    }

    public Vector2 GetPos()
    {
        return new Vector2(xPos, yPos);
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
            //mapManager.DrawMap();
            if (mapManager.roomTypes[xPos, yPos] == 0)
            {
                keepMoving = true;
            }
            else if (mapManager.roomTypes[xPos, yPos] == 1)
            {
                mapManager.encounters[xPos, yPos].SetActive(true);
                SceneManager.LoadScene("Battle");
            }
        }
        //mapManager.playerPos = new Vector2(xPos, yPos);
        //wait for key up
        if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0 && keepMoving)
        {
            canMove = true;
            keepMoving = false;
        }
    }



}
