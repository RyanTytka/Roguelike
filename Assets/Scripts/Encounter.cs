using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EncounterType { ENEMY, EVENT, SHOP, TREASURE }

public class Encounter : MonoBehaviour
{

    public EncounterType type;

    public List<GameObject> enemyInventory; //all possible enemies to choose from

    private List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> items = new List<GameObject>();
    private int eventID;
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
    }

    public List<GameObject> GetEnemies()
    {
        return enemies;
    }
}
