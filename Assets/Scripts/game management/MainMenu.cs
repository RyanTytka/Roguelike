using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject startMenuItems, CharChooseButton, statDisplay, newAbilitySelect;
    public Sprite mageSprite, warriorSprite, rogueSprite;
    public GameObject magePrefab, warriorPrefab, roguePrefab; 

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        startMenuItems.SetActive(false);
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

        //enable/disable buttons
        startMenuItems.SetActive(true);
        GameObject.Find("TitleMenuItems").SetActive(false);

        //choose random starting units to pick from
        GameObject option1 = Instantiate(CharChooseButton, new Vector3(-5, -2, 0), Quaternion.identity, GameObject.Find("HUDCanvas").transform);
        //option1.GetComponent<Button>().onClick.AddListener(delegate { loadGameScene(); });
        GameObject display1 = Instantiate(statDisplay, new Vector3(-8, 1.5f, 0), Quaternion.identity, GameObject.Find("HUDCanvas").transform);

        GameObject option2 = Instantiate(CharChooseButton, new Vector3(5, -2, 0), Quaternion.identity, GameObject.Find("HUDCanvas").transform);
        //option2.GetComponent<Button>().onClick.AddListener(delegate { loadGameScene(); });
        GameObject display2 = Instantiate(statDisplay, new Vector3(8, 1.5f, 0), Quaternion.identity, GameObject.Find("HUDCanvas").transform);
        
        //give the player 3 points to add to stats
        display1.GetComponent<DisplayStats>().modPointsLeft = 5;
        display2.GetComponent<DisplayStats>().modPointsLeft = 5;

        if (Random.Range(0, 3) <= 10)
        {
            option1.GetComponentInChildren<SpriteRenderer>().sprite = mageSprite;
            option1.GetComponent<Button>().onClick.AddListener(delegate 
            { 
                option1.GetComponent<Button>().onClick.RemoveAllListeners();
                selectCharacter(mage, option1, display1, display2, option2);
            });
            option2.GetComponentInChildren<SpriteRenderer>().sprite = warriorSprite;
            option2.GetComponent<Button>().onClick.AddListener(delegate 
            {
                option2.GetComponent<Button>().onClick.RemoveAllListeners();
                selectCharacter(warrior, option2, display2, display1, option1); 
            });
            display1.GetComponent<DisplayStats>().SetStats(mage);
            display2.GetComponent<DisplayStats>().SetStats(warrior);
        }
        else if (Random.Range(0, 2) == 0)
        {
            option1.GetComponentInChildren<SpriteRenderer>().sprite = rogueSprite;
            option1.GetComponent<Button>().onClick.AddListener(delegate 
            {
                option1.GetComponent<Button>().onClick.RemoveAllListeners();
                selectCharacter(rogue, option1, display1, display2, option2); 
            });
            option2.GetComponentInChildren<SpriteRenderer>().sprite = warriorSprite;
            option2.GetComponent<Button>().onClick.AddListener(delegate 
            {
                option2.GetComponent<Button>().onClick.RemoveAllListeners();
                selectCharacter(warrior, option2, display2, display1, option1); 
            });
            display1.GetComponent<DisplayStats>().SetStats(rogue);
            display2.GetComponent<DisplayStats>().SetStats(warrior);
        }
        else
        {
            option1.GetComponentInChildren<SpriteRenderer>().sprite = mageSprite;
            option1.GetComponent<Button>().onClick.AddListener(delegate 
            {
                option1.GetComponent<Button>().onClick.RemoveAllListeners();
                selectCharacter(mage, option1, display1, display2, option2); 
            });
            option2.GetComponentInChildren<SpriteRenderer>().sprite = rogueSprite;
            option2.GetComponent<Button>().onClick.AddListener(delegate 
            {
                option2.GetComponent<Button>().onClick.RemoveAllListeners();
                selectCharacter(rogue, option2, display2, display1, option1); 
            });
            display1.GetComponent<DisplayStats>().SetStats(mage);
            display2.GetComponent<DisplayStats>().SetStats(rogue);
        }
    }


    //go to Game scene
    public void loadGameScene()
    {
        SceneManager.LoadScene("Map");
    }

    //adds a character prefab to the party
    public void selectCharacter(GameObject playerPrefab, GameObject movePlayer, GameObject moveStats, GameObject deleteStats, GameObject deleteButton)
    {
        GameObject newPlayer = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("PlayerParty").transform);
        //move selected character to middle of screen
        movePlayer.transform.position = new Vector3(0,-2,0);
        moveStats.transform.position = new Vector3(3, 1.5f, 0);
        moveStats.GetComponent<DisplayStats>().ShowMods();
        moveStats.GetComponent<DisplayStats>().SetStats(playerPrefab);
        //delete other character
        Destroy(deleteStats);
        Destroy(deleteButton);
        //set up button to display new ability options
        movePlayer.GetComponentInChildren<Text>().text = "Confirm";
        movePlayer.GetComponentInChildren<Button>().GetComponent<RectTransform>().sizeDelta = new Vector2(60,30);
        movePlayer.GetComponentInChildren<Button>().onClick.AddListener(delegate
        {
            int[] statChanges= moveStats.GetComponent<DisplayStats>().modChanges;
            newPlayer.GetComponent<PlayerStats>().AddStats(statChanges);
            DisplayNewAbility();
            Destroy(moveStats);
        });
    }

    //randomly select and display three abilities to choose from
    public void DisplayNewAbility()
    {

        GameObject display = Instantiate(newAbilitySelect, new Vector3(0, 0), Quaternion.identity, GameObject.Find("HUDCanvas").transform);
        abilityManager abilityManager = GameObject.Find("GameManager").GetComponent<abilityManager>();

        List<GameObject> choices = abilityManager.newAbility();
        for(int i = 0; i < 3; i++)
        {
            GameObject choice = Instantiate(choices[i], new Vector3(0, i * 3, 0), Quaternion.identity);
            NewAbilitySelect select = display.GetComponent<NewAbilitySelect>();
            select.AddChoice(choice);
            PlayerAbilities playerAbilities = GetComponentInChildren<PlayerAbilities>(true);
            select.GetComponentsInChildren<Button>()[i].onClick.AddListener(delegate
            {
                playerAbilities.LearnAbility(choice);
                choice.transform.parent = playerAbilities.transform;
                loadGameScene();
            });
        }
    }
}
