using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BattleManager : MonoBehaviour
{
    public Encounter encounter;

    public List<GameObject> battlingUnits;
    private GameObject currentTurn;
    public List<GameObject> actingUnits = new List<GameObject>();

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Battle")
        {
            //get encounter info
            encounter = GetComponentInChildren<Encounter>();
            //get and activate player objects
            battlingUnits = new List<GameObject>();
            for (int i = 0; i < GameObject.Find("PlayerParty").transform.childCount; i++)
            {
                GameObject player = GameObject.Find("PlayerParty").transform.GetChild(i).gameObject;
                battlingUnits.Add(player);
                player.SetActive(true);
                //set positions of players
                player.transform.position = new Vector3(-5.0f + i * 1.5f, -2, 0);
                //refill mana
                player.GetComponent<PlayerStats>().currentMana = player.GetComponent<PlayerStats>().maxMana;
                player.GetComponent<PlayerStats>().SetBars();
            }
            //get enemies
            battlingUnits.AddRange(encounter.GetEnemies());

            //begin first turn
            actingUnits = new List<GameObject>();
            NewTurn();
        }
    }

    private void NewTurn()
    {
        //get who goes first
        while (actingUnits.Count == 0)
            actingUnits = NextTurn();

        //trigger active players turn
        currentTurn = actingUnits[0];
        currentTurn.GetComponent<ActingUnit>().MyTurn();
    }

    //when a unit has taken its turn, this is called and moves it to the next units turn
    public void TurnEnded()
    {
        if(BattleStillGoing() == false)
        {
            //end battle
            SceneManager.LoadScene("Map");
            Destroy(GetComponentInChildren<Encounter>().gameObject);
            for (int i = 0; i < GameObject.Find("PlayerParty").transform.childCount; i++)
            {
                GameObject.Find("PlayerParty").transform.GetChild(i).gameObject.SetActive(false);
            }
            //update map
            Vector2 playerPos = GetComponentInChildren<PlayerMovement>(true).GetPos();
            GetComponent<MapManager>().RoomFinished(playerPos);
        }

        actingUnits.RemoveAt(0);
        currentTurn.GetComponent<ActingUnit>().EndTurn();
        if (actingUnits.Count > 0)
        {
            //same turn, new unit acting
            currentTurn = actingUnits[0];
            currentTurn.GetComponent<ActingUnit>().MyTurn();
        }
        else
        {
            //next turn
            NewTurn();
        }
    }

    //find the unit(s) that act next
    private List<GameObject> NextTurn()
    {
        List<GameObject> turns = new List<GameObject>();
        foreach (GameObject go in battlingUnits)
        {
            if (go.GetComponent<ActingUnit>().UpdateTurn())
                turns.Add(go);
        }

        return turns;
    }

    //returns true unless all enemies or players have died
    private bool BattleStillGoing()
    {
        int pCount = 0, eCount = 0;
        foreach(GameObject go in battlingUnits)
        {
            if (go.tag == "Player")
            {
                pCount++;
            }
            else
            {
                eCount++;
            }
        }
        return pCount != 0 && eCount != 0;
    }
}
