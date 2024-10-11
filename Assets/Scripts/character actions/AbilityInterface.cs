using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityInterface : MonoBehaviour
{
    
    public enum AbilityType { ATTACK, DEFENSE, UTILITY, PASSIVE }

    //What kind of ability this is. Ex: Fire. Holy. Basic.
    public List<string> traits = new List<string>();

    //Warrior/Rogue/Mage
    public PlayerClass playerClass;

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

    public GameObject CreateStatusEffect(StatusTypeEnum type, int stacks, GameObject parent)
    {
        //check if the unit already has this type of effect
        var currentEffects = parent.GetComponentsInChildren<StatusEffect>();
        foreach(StatusEffect status in currentEffects)
        {
            if(status.type == type)
            {
                //add to current stacks
                status.stacks += stacks;
                return status.gameObject;
            }           
        }
        //create new status effect object
        GameObject obj = Instantiate(statusEffect, parent.transform);
        StatusEffect se = obj.GetComponent<StatusEffect>();
        se.type = type;
        se.stacks = stacks;
        se.statusName = se.names[(int)type];
        se.iconImage = se.icons[(int)type];
        string d = se.descriptions[(int)type];
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
            if(go.CompareTag("Enemy"))
            {
                go.GetComponentInChildren<SpriteRenderer>().color = Color.white;
            }
            else
            {
                go.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        //end turn
        selected = false;
        GameObject.Find("GameManager").GetComponent<BattleManager>().TurnEnded();
    }

    //helper methods for common target selection
    public Ray ray;
    public RaycastHit hit;
    public void TargetSelf()
    {
        if (selected)
        {
            //clear targets
            try
            {
                foreach (GameObject go in targets)
                {
                    go.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            catch { }
            targets = new List<GameObject>();
            //check if mousing over myself
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("Hit - " + hit.collider.gameObject.name);
                GameObject mouseOver = hit.collider.gameObject.transform.gameObject;
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
    public void TargetAPlayer()
    {
        if (selected)
        {
            //clear targets
            try
            {
                foreach (GameObject go in targets)
                {
                    go.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            catch { }
            targets = new List<GameObject>();
            //check if mousing over enemy
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject mouseOver = hit.collider.gameObject.transform.parent.gameObject;
                if (mouseOver.tag == "Player")
                {
                    //add hovered player to targets list
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
                GameObject mouseOver = hit.collider.gameObject.transform.parent.gameObject;
                if (mouseOver.tag == "Enemy")
                {
                    //add hovered enemy to targets list
                    targets.Add(mouseOver);
                    mouseOver.GetComponentInChildren<SpriteRenderer>().color = Color.red;
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
                        go.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                }
            }
            catch { }
            targets = new List<GameObject>();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject mouseOver = hit.collider.gameObject.transform.parent.gameObject;
                if (mouseOver.tag == "Enemy")
                {
                    //add all enemies to targets list
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    foreach (GameObject go in enemies)
                    {
                        if (go.GetComponent<UnitStats>().isDead() == false)
                        {
                            go.GetComponentInChildren<SpriteRenderer>().color = Color.red;
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

}
