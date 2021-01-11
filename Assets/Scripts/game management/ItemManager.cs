using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemManager : MonoBehaviour
{
    public GameObject newItemSelect;

    public List<GameObject> inventory; //items currently in possession of party
    public List<GameObject> allArtifacts, allWeapons, allArmor; //all items in the game

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    //randomly select and display three abilities to choose from after a battle
    public void DisplayNewItems()
    {

        GameObject display = Instantiate(newItemSelect, new Vector3(0, 0), Quaternion.identity, GameObject.Find("HUDCanvas").transform);
        List<GameObject> choices = RandomItems();

        for (int i = 0; i < 3; i++)
        {
            GameObject choice = Instantiate(choices[i], new Vector3(0, i * 3, 0), Quaternion.identity);
            NewItemSelect select = display.GetComponent<NewItemSelect>();
            select.AddChoice(choice);
            select.GetComponentsInChildren<Button>()[i].onClick.AddListener(delegate
            {
                inventory.Add(choice);
                choice.transform.parent = this.transform;
                Destroy(display);
                GameObject.Find("GameManager").GetComponent<BattleManager>().EndBattle();
            });
        }
    }
    
    //randomly select and display three abilities to choose from in the treasure rooms
    public void DisplayTreasureChoices()
    {

        GameObject display = Instantiate(newItemSelect, GameObject.Find("Canvas").transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
        print(display.transform.position);
        //display.transform.position = new Vector3(500, 200, 0);
        print(display.transform.position);
        List<GameObject> choices = RandomItems();

        for (int i = 0; i < 3; i++)
        {
            GameObject choice = Instantiate(choices[i], new Vector3(0, i * 3, 0), Quaternion.identity);
            NewItemSelect select = display.GetComponent<NewItemSelect>();
            select.AddChoice(choice);
            select.GetComponentsInChildren<Button>()[i].onClick.AddListener(delegate
            {
                inventory.Add(choice);
                choice.transform.parent = this.transform;
                Destroy(display);
                //GameObject.Find("GameManager").GetComponent<BattleManager>().EndBattle();
            });
        }
    }

    //returns 3 random items, one from each of the types
    public List<GameObject> RandomItems()
    {
        List<GameObject> temp = new List<GameObject>();

        temp.Add(allArmor[Random.Range(0, allArmor.Count)]);
        temp.Add(allWeapons[Random.Range(0, allWeapons.Count)]);
        temp.Add(allArtifacts[Random.Range(0, allArtifacts.Count)]);

        return temp;
    }

    //return randomly selected item. Higher difficulty is stronger item
    public GameObject RandomItem(float difficulty)
    {
        List<GameObject> allItems = new List<GameObject>();
        allItems.AddRange(allArmor);
        allItems.AddRange(allWeapons);
        allItems.AddRange(allArtifacts);

        return allItems[Random.Range(0, allItems.Count)];
    }
}
