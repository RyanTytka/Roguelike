using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldBash : AbilityInterface
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
                    //add hovered enemy to targets list
                    targets.Add(mouseOver);
                    mouseOver.GetComponent<SpriteRenderer>().color = Color.red;
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
            obj.GetComponent<UnitStats>().TakeDamage(caster.GetComponent<PlayerStats>().Armor, 1);
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
        GameObject.Find("History").GetComponent<BattleHistory>().AddLog(caster.GetComponent<PlayerStats>().playerName + " uses Shield Bash.");
    }

    public override string GetDescription()
    {
        string atk;
        if (caster == null)
        {
            atk = "(Armor)";
        }
        else
        {
            atk = caster.GetComponent<PlayerStats>().Armor.ToString();
        }
        return "Bash an enemy with your shield, dealing " + atk + " physical damage.";
    }
}
