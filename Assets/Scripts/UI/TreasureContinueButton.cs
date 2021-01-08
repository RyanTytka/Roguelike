using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TreasureContinueButton : MonoBehaviour
{
    //return to map scene
    public void Continue()
    {
        //update map
        GameObject manager = GameObject.Find("GameManager");
        Vector2 playerPos = manager.GetComponentInChildren<PlayerMovement>(true).GetPos();
        manager.GetComponent<MapManager>().RoomFinished(playerPos);
        
        SceneManager.LoadScene("Map");
    }
}
