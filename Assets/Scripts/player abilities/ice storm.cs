using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icestorm : AbilityInterface
{
    Ray ray;
    RaycastHit hit;

    void Update()
    {
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

            if (Input.GetMouseButtonDown(0))
            {
                if (targets.Count > 0)
                    Use();
            }
        }
    }

    public override void Use()
    {
        foreach (GameObject obj in targets)
        {
            obj.GetComponent<UnitStats>().TakeDamage(caster.GetComponent<PlayerStats>().Magic * 0.5f, 1);
            CreateStatusEffect(StatusType.SPEED_DOWN, 2, 0, target);
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
        GameObject.Find("History").GetComponent<BattleHistory>().AddLog(caster.GetComponent<PlayerStats>().playerName + " uses ice storm.");
    }

    public override string GetDescription()
    {
        string atk;
        if (caster == null)
        {
            atk = "(Magic * 0.5)";
        }
        else
        {
            //rdt - need to be able to show half magic 
            atk = caster.GetComponent<PlayerStats>().Magic.ToString();
        }
        return "Deal " + atk + " magic damage to all enemies and apply Speed Down 2 to them.";
    }
}
