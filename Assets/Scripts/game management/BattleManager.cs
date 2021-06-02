using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BattleManager : MonoBehaviour
{
    public Encounter encounter;

    public GameObject continueButton;
    public GameObject lootText;
    public GameObject itemLoot;

    public List<GameObject> battlingUnits;
    private GameObject currentTurn;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Battle")
        {
            continueButton = GameObject.Find("ContinueButton");
            continueButton.SetActive(false);
            lootText = GameObject.Find("LootText");
            lootText.SetActive(false);            
            itemLoot = GameObject.Find("ItemLoot");
            itemLoot.SetActive(false);
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
            //init turn tracker
            GameObject.Find("TurnTracker").GetComponent<TurnTracker>().allUnits = battlingUnits;
            GameObject.Find("TurnTracker").GetComponent<TurnTracker>().Init();
            //begin first turn
            NewTurn();
        }
    }

    //call once loot has been collected
    public void EndBattle()
    {
        //after boss, recruit character. Otherwise go back to map
        if (encounter.type == EncounterType.BOSS)
        {
            SceneManager.LoadScene("NewCharacter");
            GameObject party = GameObject.Find("PlayerParty");
            party.GetComponent<PartyManager>().ChooseNewCharacter();
        }
        else
        {
            SceneManager.LoadScene("Map");
        }
        Destroy(GetComponentInChildren<Encounter>().gameObject);
        for (int i = 0; i < GameObject.Find("PlayerParty").transform.childCount; i++)
        {
            GameObject.Find("PlayerParty").transform.GetChild(i).gameObject.SetActive(false);
        }
        //update map
        Vector2 playerPos = GetComponentInChildren<PlayerMovement>(true).GetPos();
        GetComponent<MapManager>().RoomFinished(playerPos);
        //xp
        GameObject.Find("PlayerParty").GetComponent<PartyManager>().AddXp(3);
    }

    private void NewTurn()
    {
        //update status effect icons
        foreach(GameObject go in battlingUnits)
        {
            go.GetComponent<ActingUnit>().UpdateStatusEffects();
        }
        //get who goes next
        currentTurn = GameObject.Find("TurnTracker").GetComponent<TurnTracker>().NextTurn();
        //trigger active players turn
        currentTurn.GetComponent<ActingUnit>().MyTurn();

    }

    //when a unit has taken its turn, this is called and moves it to the next units turn
    public void TurnEnded()
    {
        if(BattleStillGoing() == false)
        {
            //clear battling units
            battlingUnits.Clear();

            //display gold
            int goldLoot = encounter.GetComponent<Encounter>().gold;
            lootText.GetComponent<Text>().text = goldLoot + " gold looted";
            GameObject.Find("PlayerParty").GetComponent<PartyManager>().Gold += goldLoot;
            lootText.SetActive(true);

            //if the battle awards an item
            if(encounter.GetComponent<Encounter>().items != null && encounter.GetComponent<Encounter>().items.Count > 0)
            {
                //display item image and text
                GameObject item = Instantiate(encounter.GetComponent<Encounter>().items[0]);
                itemLoot.SetActive(true);
                itemLoot.GetComponentInChildren<Text>().text = item.GetComponent<ItemInterface>().itemName + " found!";
                itemLoot.GetComponentInChildren<Image>().sprite= item.GetComponent<ItemInterface>().image;
                GameObject itemMan = GameObject.Find("ItemManager");
                itemMan.GetComponent<ItemManager>().inventory.Add(item);
                item.transform.SetParent(itemMan.transform);
            }

            //continue button
            continueButton.SetActive(true);
            continueButton.GetComponent<Button>().onClick.AddListener(EndBattle);
        }

        currentTurn.GetComponent<ActingUnit>().EndTurn();
        NewTurn();
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
            else if (go.tag == "Enemy")
            {
                eCount++;
            }
        }
        return pCount != 0 && eCount != 0;
    }
}
