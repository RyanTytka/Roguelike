using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BattleManager : MonoBehaviour
{
    public Encounter encounter;

    private List<GameObject> battlingUnits;
    private GameObject currentTurn;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Battle")
        {
            encounter = GetComponentInChildren<Encounter>();

            //one round of turns
            List<GameObject> actingUnits = nextTurn();
            while(actingUnits.Count > 0)
            {
                
            }
        }
    }

    //find the unit(s) that act next
    private List<GameObject> nextTurn()
    {
        List<GameObject> turns = new List<GameObject>();

        foreach(GameObject go in battlingUnits)
        {
            if(go.tag == "Player")
            {
                if (go.GetComponent<PlayerStats>().UpdateTurn())
                    turns.Add(go);
            }
            else
            {
                if(go.GetComponent<UnitStats>().UpdateTurn())
                    turns.Add(go);
            }
        }

        return turns;
    }
}
