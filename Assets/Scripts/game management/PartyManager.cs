﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PartyManager : MonoBehaviour
{
    public GameObject CharChooseButton;
    public GameObject newAbilitySelect;
    public GameObject statDisplay;
    public GameObject partyMenuPrefab;
    private GameObject partyMenu;
    public Sprite mageSprite, warriorSprite, rogueSprite;
    public GameObject magePrefab, warriorPrefab, roguePrefab;
    public bool partyMenuOpen;

    private int gold;
    public int Gold 
    {
        get { return gold; }
        set 
        { 
            gold = value;
            if(GameObject.Find("GoldIcon") != null)
                GameObject.Find("GoldIcon").GetComponentInChildren<Text>().text = gold.ToString();
        }
    }

    public List<GameObject> party = new List<GameObject>();

    private void Start()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameObject.Find("GoldIcon") != null)
            GameObject.Find("GoldIcon").GetComponentInChildren<Text>().text = gold.ToString();

        if (scene.name == "Map")
            GameObject.Find("PartyMenuButton").GetComponent<Button>().onClick.AddListener(delegate { OpenPartyMenu(); });
        else if (scene.name == "Treasure")
        {
            GameObject.Find("ItemManager").GetComponent<ItemManager>().DisplayTreasureChoices();
        }
        else if (scene.name == "NewCharacter")
        {
            ShowStartItems();
        }
        else if (scene.name == "LevelUp")
        {

        }

    }

    //adds xp to all players
    public void AddXp(float amount)
    {
        foreach (GameObject player in party)
        {
            player.GetComponent<PlayerStats>().GainXP(amount);
        }
    }

    //open menu where player can edit characters and party
    public void OpenPartyMenu()
    {
        if(partyMenuOpen == false)
        {
            partyMenu = Instantiate(partyMenuPrefab, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
            partyMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            partyMenu.GetComponent<PartyMenu>().Display(party);
            partyMenuOpen = true;
        }
    }

    //adds character to party
    public void AddCharacter(GameObject character)
    {
        party.Add(character);
        character.transform.parent = transform;
    }

    //creates and displays two characters for player to choose from to add to party
    public void ChooseNewCharacter()
    {
    }

    //display start run items
    public void ShowStartItems()
    {
        //create player prefabs to be added
        GameObject mage = Instantiate(magePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject warrior = Instantiate(warriorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject rogue = Instantiate(roguePrefab, new Vector3(0, 0, 0), Quaternion.identity);

        //randomize stats
        mage.GetComponent<PlayerStats>().RandomizeStats();
        warrior.GetComponent<PlayerStats>().RandomizeStats();
        rogue.GetComponent<PlayerStats>().RandomizeStats();

        //choose random starting units to pick from
        GameObject option1 = Instantiate(CharChooseButton, new Vector3(-5, -2, 0), Quaternion.identity, GameObject.Find("HUDCanvas").transform);
        GameObject display1 = Instantiate(statDisplay, new Vector3(-5, -3.5f, 0), Quaternion.identity, GameObject.Find("HUDCanvas").transform);

        GameObject option2 = Instantiate(CharChooseButton, new Vector3(0, -2, 0), Quaternion.identity, GameObject.Find("HUDCanvas").transform);
        GameObject display2 = Instantiate(statDisplay, new Vector3(0, -3.5f, 0), Quaternion.identity, GameObject.Find("HUDCanvas").transform);
        
        GameObject option3 = Instantiate(CharChooseButton, new Vector3(5, -2, 0), Quaternion.identity, GameObject.Find("HUDCanvas").transform);
        GameObject display3 = Instantiate(statDisplay, new Vector3(5, -3.5f, 0), Quaternion.identity, GameObject.Find("HUDCanvas").transform);

        option1.GetComponentInChildren<SpriteRenderer>().sprite = mageSprite;
        option1.GetComponent<Button>().onClick.AddListener(delegate
        {
            option1.GetComponent<Button>().onClick.RemoveAllListeners();
            selectCharacter(mage, option1, display1, display2, option2, display3, option3);
        });
        option2.GetComponentInChildren<SpriteRenderer>().sprite = warriorSprite;
        option2.GetComponent<Button>().onClick.AddListener(delegate
        {
            option2.GetComponent<Button>().onClick.RemoveAllListeners();
            selectCharacter(warrior, option2, display2, display1, option1, display3, option3);
        });
        option3.GetComponentInChildren<SpriteRenderer>().sprite = rogueSprite;
        option3.GetComponent<Button>().onClick.AddListener(delegate
        {
            option2.GetComponent<Button>().onClick.RemoveAllListeners();
            selectCharacter(rogue, option3, display3, display1, option1, display2, option2);
        });
        display1.GetComponent<DisplayStats>().SetStats(mage);
        display2.GetComponent<DisplayStats>().SetStats(warrior);
        display3.GetComponent<DisplayStats>().SetStats(rogue);

        //give the player 3 points to add to stats
        //display1.GetComponent<DisplayStats>().modPointsLeft = 5;
        //display2.GetComponent<DisplayStats>().modPointsLeft = 5;
    }

    //adds a character prefab to the party
    public void selectCharacter(GameObject playerPrefab, GameObject movePlayer, GameObject moveStats, 
        GameObject deleteStats1, GameObject deleteButton1, GameObject deleteStats2, GameObject deleteButton2)
    {
        GameObject newPlayer = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        AddCharacter(newPlayer);
        //move selected character to middle of screen
        movePlayer.transform.position = new Vector3(0, -2, 0);
        moveStats.transform.position = new Vector3(0, -3.5f, 0);
        moveStats.GetComponent<DisplayStats>().ShowMods();
        moveStats.GetComponent<DisplayStats>().SetStats(playerPrefab);
        //delete other characters
        Destroy(deleteStats1);
        Destroy(deleteButton1);
        Destroy(deleteStats2);
        Destroy(deleteButton2);
        Destroy(movePlayer);
        //set up button to display new ability options
        //movePlayer.GetComponentInChildren<Text>().text = "Confirm";
        //movePlayer.GetComponentInChildren<Button>().GetComponent<RectTransform>().sizeDelta = new Vector2(60, 30);
        //movePlayer.GetComponentInChildren<Button>().onClick.AddListener(delegate
        //{
        //    int[] statChanges = moveStats.GetComponent<DisplayStats>().modChanges;
        //    newPlayer.GetComponent<PlayerStats>().AddStats(statChanges);
        //});

        //Display new ability
        GameObject.Find("ChooseText").GetComponent<Text>().text = "Choose an ability to learn";
        GameObject abilityDisplay = Instantiate(newAbilitySelect, new Vector3(0, 0), Quaternion.identity, GameObject.Find("HUDCanvas").transform);
        abilityManager abilityManager = GameObject.Find("AbilityManager").GetComponent<abilityManager>();
        //set image to chosen player image
        abilityDisplay.GetComponentsInChildren<Image>()[3].sprite = playerPrefab.GetComponent<SpriteRenderer>().sprite;
        //select 3 random abilities
        List<GameObject> choices = abilityManager.newAbility();
        for (int i = 0; i < 3; i++)
        {
            GameObject choice = Instantiate(choices[i], new Vector3(0, i * 3, 0), Quaternion.identity);
            NewAbilitySelect select = abilityDisplay.GetComponent<NewAbilitySelect>();
            select.AddChoice(choice);
            PlayerAbilities[] playerAbilitiesList = GetComponentsInChildren<PlayerAbilities>(true);
            PlayerAbilities playerAbilities = GetComponentsInChildren<PlayerAbilities>(true)[playerAbilitiesList.Length - 1];
            select.GetComponentsInChildren<Button>()[i].onClick.AddListener(delegate
            {
                playerAbilities.LearnAbility(choice);
                choice.transform.parent = playerAbilities.transform;

                //create new map and load it
                GameObject.Find("GameManager").GetComponent<MapManager>().CreateMap();
                GameObject.Find("GameManager").GetComponentInChildren<PlayerMovement>(true).Init();

                SceneManager.LoadScene("Map");
            });
        }
    }
}
