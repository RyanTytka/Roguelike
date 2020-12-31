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

    //randomly select and display three abilities to choose from
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

    //returns 3 random artifacts from the list of all artifacts
    public List<GameObject> RandomItems()
    {
        List<GameObject> temp = new List<GameObject>();
        List<int> used = new List<int>();
        int index;

        index = Random.Range(0, allArtifacts.Count);
        temp.Add(allArtifacts[index]);
        used.Add(index);

        while (used.Contains(index))
            index = Random.Range(0, allArtifacts.Count);
        used.Add(index);
        temp.Add(allArtifacts[index]);

        while (used.Contains(index))
            index = Random.Range(0, allArtifacts.Count);
        used.Add(index);
        temp.Add(allArtifacts[index]);

        return temp;
    }
}
