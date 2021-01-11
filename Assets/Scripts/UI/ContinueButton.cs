using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    //return to map scene
    public void Continue(string SceneName)
    {
        //update map
        try
        {
            GameObject manager = GameObject.Find("GameManager");
            Vector2 playerPos = manager.GetComponentInChildren<PlayerMovement>(true).GetPos();
            manager.GetComponent<MapManager>().RoomFinished(playerPos);
        } catch { }
        
        SceneManager.LoadScene(SceneName);
    }
}
