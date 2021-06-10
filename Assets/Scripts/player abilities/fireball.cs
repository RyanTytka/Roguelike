using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : AbilityInterface
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
            }
            catch { }
            targets.Clear();
            //check if mousing over enemy
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject mouseOver = hit.collider.gameObject;
                if (mouseOver.tag == "Enemy")
                {
                    //get list of enemies
                    BattleManager bm = GameObject.Find("GameManager").GetComponent<BattleManager>();
                    List<GameObject> enemies = bm.battlingUnits.FindAll(unit => unit.tag == "Enemy");
                    //add adjacent enemies to targets list
                    int index = enemies.IndexOf(mouseOver);
                    enemies[index].GetComponent<SpriteRenderer>().color = Color.red;
                    targets.Add(enemies[index]);
                    if (index > 0)
                    {
                        var leftUnit = enemies[index - 1];
                        leftUnit.GetComponent<SpriteRenderer>().color = Color.red;
                        targets.Add(leftUnit);
                    }
                    if (index < enemies.Count - 1)
                    {
                        var rightUnit = enemies[index + 1];
                        rightUnit.GetComponent<SpriteRenderer>().color = Color.red;
                        targets.Add(rightUnit);
                    }
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (targets.Count > 0)
                    Use();
            }
        }
    }

    public override void Use()
    {
        caster.GetComponent<PlayerStats>().UseMana(manaCost);
        Debug.Log("Fireball used");
        foreach (GameObject obj in targets)
        {
            obj.GetComponent<UnitStats>().TakeDamage(caster.GetComponent<PlayerStats>().Magic, 2);
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
    }
}
