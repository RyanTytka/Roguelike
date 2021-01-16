using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EncounterType { ENEMY, EVENT, SHOP, TREASURE }

public class Encounter : MonoBehaviour
{

    public EncounterType type;

    public List<GameObject> enemyInventory; //all possible enemies to choose from
    public GameObject shopItem;

    private List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> items = new List<GameObject>();
    public int eventID;
    public int gold;

    public void GenerateEncounter(float difficulty)
    {
        int xPos = 3;
        if(type == EncounterType.ENEMY)
        {
            float totalDifficulty = 0;
            float targetDifficulty = difficulty + Random.Range(0.0f, 0.5f) * difficulty;
            while(totalDifficulty < targetDifficulty)
            {
                int enemyNum = Random.Range(0, enemyInventory.Count);
                GameObject enemy = Instantiate(enemyInventory[enemyNum], new Vector3(xPos, -2, 10), 
                    Quaternion.identity, this.transform);
                totalDifficulty += enemy.GetComponent<Enemy>().difficulty;
                enemies.Add(enemy);
                xPos += 2;
            }
            gold = (int)Random.Range(5 * difficulty, 5 * difficulty + 5);
            if(Random.Range(0.0f, 1.0f) < 0.25f)
            {
                //award random item at end of fight
                items.Add(GameObject.Find("ItemManager").GetComponent<ItemManager>().RandomItem(difficulty));
            }
        }
        else if(type == EncounterType.TREASURE)
        {
            gold = (int)Random.Range(15 * difficulty, 15 * difficulty + 10);
            //ItemManager im = GameObject.Find("ItemManager").GetComponent<ItemManager>();
            //items.AddRange(im.RandomItems());
        }
        else if(type == EncounterType.SHOP)
        {
            List<string> added = new List<string>();
            for(int i = 0; i < 5; i++)
            {
                GameObject tempItem = GameObject.Find("ItemManager").GetComponent<ItemManager>().RandomItem(difficulty);
                if(added.Contains(tempItem.GetComponent<ItemInterface>().itemName))
                {
                    //duplicate
                    i--;
                }
                else
                {
                    //add item
                    items.Add(tempItem);
                    added.Add(tempItem.GetComponent<ItemInterface>().itemName);
                }
            }
            //print("items:");
            //foreach (GameObject go in items)
                //print(go.GetComponent<ItemInterface>().itemName);
        }
        else if(type == EncounterType.EVENT)
        {
            eventID = Random.Range(0, 3);
            switch (eventID)
            {
                case 0: //treasure room
                    type = EncounterType.TREASURE;
                    GenerateEncounter(difficulty);
                    type = EncounterType.EVENT;
                    break;
                case 1: //enemy room
                    type = EncounterType.ENEMY;
                    GenerateEncounter(difficulty);
                    type = EncounterType.EVENT;
                    break;
                case 2: //heal party

                    break;
            }
        }
    }

    //when player moves to an event tile, this sets up specific event
    public void LoadEvent()
    {
        switch (eventID)
        {
            case 0: //treasure room
                SceneManager.LoadScene("Treasure");
                break;
            case 1: //enemy room
                SceneManager.LoadScene("Battle");
                break;
            case 2: //heal party
                SceneManager.LoadScene("Event");
                break;
        }
    }

    public List<GameObject> GetEnemies()
    {
        return enemies;
    }

    //adds shopItems to the scene
    public void DisplayShop()
    {
        for(int i = 0; i < items.Count; i++)
        {
            GameObject item = Instantiate(shopItem, GameObject.Find("Canvas").transform);
            item.transform.position = new Vector3(50, 375 + i * -65, 0);
            int cost = Random.Range(10, 15);
            item.GetComponent<ShopItem>().cost = cost;
            item.GetComponent<ShopItem>().costText.text = cost.ToString();
            item.GetComponent<ShopItem>().nameText.text = items[i].GetComponent<ItemInterface>().itemName;
            item.GetComponent<ShopItem>().displayImage.sprite = items[i].GetComponent<ItemInterface>().image;
            item.GetComponent<ShopItem>().itemRef = Instantiate(items[i]);
        }
    }
}
