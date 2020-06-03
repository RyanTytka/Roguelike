using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject startMenuItems, CharChooseButton, statDisplay;
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
        //enable/disable buttons
        startMenuItems.SetActive(true);
        GameObject.Find("TitleMenuItems").SetActive(false);

        //choose random starting units to pick from
        GameObject option1 = Instantiate(CharChooseButton, new Vector3(-5, -2, 0), Quaternion.identity, GameObject.Find("HUDCanvas").transform);
        option1.GetComponent<Button>().onClick.AddListener(delegate { loadGameScene(); });
        GameObject display1 = Instantiate(statDisplay, new Vector3(-8, 0, 0), Quaternion.identity, GameObject.Find("HUDCanvas").transform);
        //display1.GetComponent<DisplayStats>().SetStats();

        GameObject option2 = Instantiate(CharChooseButton, new Vector3(5, -2, 0), Quaternion.identity, GameObject.Find("HUDCanvas").transform);
        option2.GetComponent<Button>().onClick.AddListener(delegate { loadGameScene(); });
        GameObject display2 = Instantiate(statDisplay, new Vector3(8, 0, 0), Quaternion.identity, GameObject.Find("HUDCanvas").transform);

        if (Random.Range(0, 3) == 0)
        {
            option1.GetComponentInChildren<SpriteRenderer>().sprite = mageSprite;
            option1.GetComponent<Button>().onClick.AddListener(delegate { addCharacter(1); });
            option2.GetComponentInChildren<SpriteRenderer>().sprite = warriorSprite;
            option2.GetComponent<Button>().onClick.AddListener(delegate { addCharacter(2); });
        }
        else if (Random.Range(0, 2) == 0)
        {
            option1.GetComponentInChildren<SpriteRenderer>().sprite = rogueSprite;
            option1.GetComponent<Button>().onClick.AddListener(delegate { addCharacter(3); });
            option2.GetComponentInChildren<SpriteRenderer>().sprite = warriorSprite;
            option2.GetComponent<Button>().onClick.AddListener(delegate { addCharacter(2); });
        }
        else
        {
            option1.GetComponentInChildren<SpriteRenderer>().sprite = mageSprite;
            option1.GetComponent<Button>().onClick.AddListener(delegate { addCharacter(1); });
            option2.GetComponentInChildren<SpriteRenderer>().sprite = rogueSprite;
            option2.GetComponent<Button>().onClick.AddListener(delegate { addCharacter(3); });
        }
    }


    //go to Game scene
    public void loadGameScene()
    {
        SceneManager.LoadScene("Map");
    }

    //adds a character prefab to the party
    public void addCharacter(int type)
    {
        if(type == 1)
        {
            Instantiate(magePrefab, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("PlayerParty").transform);
        }
        if (type == 2)
        {
            Instantiate(warriorPrefab, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("PlayerParty").transform);
        }
        if (type == 3)
        {
            Instantiate(roguePrefab, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("PlayerParty").transform);
        }
    }
}
