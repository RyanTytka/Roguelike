using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityInterface : MonoBehaviour
{
    
    public enum AbilityType { ATTACK, DEFENSE, UTILITY }

    //What kind of ability this is. Ex: Fire. Holy. Basic.
    public List<string> traits = new List<string>(); 

    //Used to select characters that are being targeted
    public List<GameObject> targets = new List<GameObject>();

    //Who is using this ability
    public GameObject caster;
    protected PlayerStats playerStats;

    public float manaCost;
    public AbilityType abilityType;
    public string abilityName, description;
    public Sprite image;

    public bool selected = false;

    //blank status effect prefab
    public GameObject statusEffect; 
    
    public void SetTargets(List<GameObject> targets)
    {
        this.targets = targets;
    }

    public abstract void Use();
    public abstract string GetDescription();

    public GameObject CreateStatusEffect(StatusTypeEnum type, int stacks, int iconID, GameObject parent)
    {
        //check if the unit already has this type of effect
        var currentEffects = GetComponentsInChildren<StatusEffect>();
        foreach(StatusEffect se in currentEffects)
        {
            if(se.type == type)
            {
                //add to current stacks
                se.stacks += stacks;
                return se.gameObject;
            }           
        }
        //create new status effect object
        GameObject obj = Instantiate(statusEffect, parent.transform);
        StatusEffect se = obj.GetComponent<StatusEffect>();
        se.type = type;
        se.stacks = stacks;
        se.statusName = se.names[iconID];
        se.iconImage = se.icons[iconID];
        string d = se.descriptions[iconID];
        obj.GetComponent<StatusEffect>().description = d;
        return obj;
    }

    //clear targets and end turn after an ability has been used
    public void AbilityUsed()
    {
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

    //helper methods for common target selection
    Ray ray;
    RaycastHit hit;
    public void TargetSelf()
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
            //check if mousing over myself
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject mouseOver = hit.collider.gameObject;
                if (mouseOver == caster)
                {
                    //add myself to targets list
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
    public void TargetAPlyaer()
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
                if (mouseOver.tag == "Player")
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
    public void TargetAnEnemy()
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
    public void TargetAllEnemies()
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
            //check if mousing over myself
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject mouseOver = hit.collider.gameObject;
                if (mouseOver == caster)
                {
                    //add myself to targets list
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

}
