using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using E = EnemyTypeEnum;
public enum EncounterType { ENEMY, EVENT, SHOP, TREASURE, BOSS }
public class Encounter : MonoBehaviour
{

    public EncounterType type;

    // all possible enemies to choose from
    public List<GameObject> enemyInventory; 
    
    // one list item is an array that contains the ids for each enemy in that boss encounter
    public List<int[]> bossEncounters = new List<int[]> { 
        new int[] { (int)E.BONE_PILE, (int)E.BONE_PILE, (int)E.SKELETON_KING },
        new int[] { (int)E.LICH_KING },
        new int[] { (int)E.ORC_KING },
        new int[] { (int)E.BEHOLDER },
        new int[] { (int)E.ICE_DRAGON },
    };

    // similar types of enemies spawn together
    public List<int[]> buckets = new List<int[]> { 
        //cultists
        new int[] { (int)E.TENTACLE, (int)E.CULTIST },
        //undead 
        new int[] { (int) E.ZOMBIE, (int) E.LICH, (int) E.SKELETON, (int) E.SOUL_COLLECTOR, (int) E.SKELETON_KNIGHT, (int) E.SKELETON_LORD },
        //spirits
        new int[] { (int)E.GHOST_WIZARD, (int)E.GHOST_HOUND, (int)E.WRAITH },
        //demons
        new int[] { (int)E.CHAIN_DEMON, (int)E.VOID_DEMON, (int)E.FLYING_EYE, (int)E.IMP },
        //nature
        new int[] { (int)E.CARNIVOROUS_PLANT, (int)E.GIANT_SPIDER, (int)E.HERMIT, (int)E.MONSTER_PLANT, (int)E.MUSHROOM, (int)E.WATER_SPIRIT },
        //constructs
        new int[] { (int)E.CLAY_GOLEM, (int)E.STONE_GOLEM, (int)E.ICE_GOLEM },
        //creatures
        new int[] { (int)E.HOUND, (int)E.SNAKE, (int)E.WENDIGO, (int)E.WEREWOLF, (int)E.VAMPIRE },
        //humanoid
        new int[] { (int)E.GOBLIN, (int)E.ORC, (int)E.WIZARD },
        //ooze
        new int[] { (int)E.OOZE, (int)E.TOXIC_OOZE }
    };

private List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> items = new List<GameObject>();
    public int eventID;
    public int gold;
    public GameObject shopItem;

    public void GenerateEncounter(float difficulty)
    {
        int xPos = 3;
        if(type == EncounterType.ENEMY)
        {
            float totalDifficulty = 0;
            float targetDifficulty = difficulty + Random.Range(0.0f, 0.5f) * difficulty;
            //choose a bucket
            int[] bucket = buckets[(int)Random.Range(0.0f, buckets.Count - 1)];
            while(totalDifficulty < targetDifficulty)
            {
                int enemyNum = Random.Range(0, bucket.Length);
                GameObject enemy = Instantiate(enemyInventory[bucket[enemyNum]], new Vector3(xPos, -2, 10), Quaternion.identity, this.transform);
                if (enemy.GetComponent<Enemy>().isBoss == false)
                {
                    totalDifficulty += enemy.GetComponent<Enemy>().difficulty;
                    enemies.Add(enemy);
                    xPos += 2;
                }
                else
                {
                    Destroy(enemy);
                }
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
        else if(type == EncounterType.BOSS)
        {
            int bossNum = Random.Range(0, bossEncounters.Count);
            foreach (int i in bossEncounters[bossNum])
            {
                GameObject enemy = Instantiate(enemyInventory[i], new Vector3(xPos, -2, 10),
                    Quaternion.identity, this.transform);
                enemies.Add(enemy);
                xPos += 2;
            }
            gold = (int)Random.Range(10 * difficulty, 10 * difficulty + 5);
            //award random item at end of fight
            items.Add(GameObject.Find("ItemManager").GetComponent<ItemManager>().RandomItem(difficulty));
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
