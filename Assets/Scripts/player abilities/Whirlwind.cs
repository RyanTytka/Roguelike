using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : AbilityInterface
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
            targets = new List<GameObject>();
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
        caster.GetComponent<PlayerStats>().UseMana(manaCost);
        //Debug.Log("Whirlwind used");
        foreach(GameObject obj in targets)
        {
            obj.GetComponent<UnitStats>().TakeDamage(caster.GetComponent<PlayerStats>().attack);
        }
        //clear targets
        caster.GetComponent<PlayerAbilities>().Hide();
        foreach (GameObject go in targets)
        {
            if(go.GetComponent<UnitStats>().isDead() == false)
                go.GetComponent<SpriteRenderer>().color = Color.white;
        }
        //end turn
        selected = false;
        GameObject.Find("GameManager").GetComponent<BattleManager>().TurnEnded();
    }
}
