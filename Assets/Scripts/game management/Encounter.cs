using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EncounterType { ENEMY, EVENT, SHOP, TREASURE }

public class Encounter : MonoBehaviour
{

    public EncounterType type;

    public List<GameObject> enemyInventory; //all possible enemies to take from

    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> items = new List<GameObject>();
    private int eventID;

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
        }
    }

    public List<GameObject> GetEnemies()
    {
        return enemies;
    }
}
