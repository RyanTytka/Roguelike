using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frenzy : AbilityInterface
{
    Ray ray;
    RaycastHit hit;

    void Update()
    {
        //if (GetComponent<PlayerStats>().currentMana >= ability.GetComponent<AbilityInterface>().manaCost)
        {

        }
        if (selected)
        {
            //clear targets
            try
            {
                foreach (GameObject go in targets)
                {
                    if (go.GetComponent<UnitStats>().isDead() == false)
                        go.GetComponent<SpriteRenderer>().color = Color.white;
                }
            } catch { }
            targets.Clear();
            //check if mousing over enemy
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject mouseOver = hit.collider.gameObject;
                if (mouseOver.tag == "Enemy")
                {
                    //add all enemies to targets list
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    foreach(GameObject go in enemies)
                    {
                        if (go.GetComponent<UnitStats>().isDead() == false)
                        {
                            go.GetComponent<SpriteRenderer>().color = Color.red;
                            targets.Add(go);
                        }
                    }
                }
            }

            if(Input.GetMouseButtonDown(0))
            {
                if(targets.Count > 0)
                    Use();
            }
        }
    }

    public override void Use()
    {
        for(int i = 0; i < 3; i++)
        {
            //choose a random enemy
            int selectedEnemy = Random.Range(0, targets.Count);
            targets[selectedEnemy].GetComponent<UnitStats>().TakeDamage(caster.GetComponent<PlayerStats>().Attack, 1);
        }
        //clear targets
        caster.GetComponent<PlayerAbilities>().Hide();
        foreach (GameObject go in targets)
        {
            if (go.GetComponent<UnitStats>().isDead() == false)
                go.GetComponent<SpriteRenderer>().color = Color.white;
        }
        //end turn
        selected = false;
        GameObject.Find("GameManager").GetComponent<BattleManager>().TurnEnded();

        //Update History
        GameObject.Find("History").GetComponent<BattleHistory>().AddLog(caster.GetComponent<PlayerStats>().playerName + " uses Frenzy.");
    }

    public override string GetDescription()
    {
        string atk;
        if (caster == null)
        {
            atk = "(Attack)";
        }
        else
        {
            atk = caster.GetComponent<PlayerStats>().Attack.ToString();
        }
        return "Attack a random enemy three times, dealing " + atk + " physical damage each.";
    }
}
