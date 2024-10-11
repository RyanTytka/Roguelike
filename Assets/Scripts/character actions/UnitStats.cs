using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class UnitStats : ActingUnit, IComparable
{

    private bool dead = false;

    public GameObject statusEffectIconPrefab; //instantiated to show what statuses are affecting this unit
    private List<GameObject> statusEffectIcons = new List<GameObject>(); //keeps track of the objects created to show current effects

    private GameObject activeHoverDisplay;

    public GameObject[] prefabs; //prefabs that might need to be instantiated
    private const int BONE_PILE = 0;

    public void calculateNextActTurn(int currentTurn)
    {
        this.turnTimer = currentTurn + (int)Math.Ceiling(100.0f / this.speed);
    }

    public int CompareTo(object otherStats)
    {
        return turnTimer.CompareTo(((UnitStats)otherStats).turnTimer);
    }

    public bool isDead()
    {
        return this.dead;
    }

    public override void MyTurn()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        //highlight me
        //GetComponent<SpriteRenderer>().color = Color.yellow;

        //pause
        yield return new WaitForSeconds(0.5f);
        
        //use an ability
        GetComponent<Enemy>().TakeTurn();

        yield return new WaitForSeconds(0.5f);

        //end turn
        GameObject.Find("GameManager").GetComponent<BattleManager>().TurnEnded();
    }

    public override void EndTurn()
    {
        //GetComponent<SpriteRenderer>().color = Color.white;
    }

    //update health bar with current heralth value
    public void SetHealthBar()
    {
        GetComponentInChildren<HealthBar>().CurrentValue = currentHealth;
        GetComponentInChildren<HealthBar>().MaxValue = maxHealth;
        GetComponentInChildren<Text>().text = currentHealth.ToString("N1") + "/" + maxHealth;
    }

    public void TakeDamage(float damage, int type)
    {
        //defense/resilience
        if (type == 1)
        {
            damage *= 10 / (10 + Defense);
        }
        else if (type == 2)
        {
            damage *= 10 / (10 + Resilience);
        }

        //print(gameObject.name + " took " + damage + " damage");
        currentHealth -= damage;
        SetHealthBar();

        //create damage text
        var obj = Instantiate(GameObject.Find("GameManager").GetComponent<UIManager>().damageTextPrefab, GameObject.Find("HUDCanvas").transform);
        Color c = Color.red;
        if (type == 2)
            c = Color.blue;
        obj.GetComponent<DamageText>().Init("-", damage, c, transform.position);

        //if dead
        if (currentHealth <= 0)
        {
            if(unitName == "Undying Soldier")
            {
                //summon pile of bones in this place
                BattleManager bm = GameObject.Find("GameManager").GetComponent<BattleManager>();
                Encounter encounter = bm.gameObject.GetComponentInChildren<Encounter>();
                GameObject newBonePile = Instantiate(prefabs[BONE_PILE], new Vector3(bm.battlingUnits.Count * 2, -2, 10), Quaternion.identity, encounter.transform);
                bm.battlingUnits[bm.battlingUnits.IndexOf(this.gameObject)] = newBonePile;
                GameObject.Find("TurnTracker").GetComponent<TurnTracker>().UnitDied(this.gameObject);
                Destroy(this.gameObject);
                //restructure unit order
                GameObject.Find("GameManager").GetComponent<BattleManager>().UpdateUnitPositions();
            }
            else
            {
                dead = true;
                GameObject.Find("GameManager").GetComponent<BattleManager>().battlingUnits.Remove(this.gameObject);
                GameObject.Find("TurnTracker").GetComponent<TurnTracker>().UnitDied(this.gameObject);
                //GetComponent<SpriteRenderer>().color = Color.gray;
                Destroy(this.gameObject);
            }
        }

        //Update History
        GameObject.Find("History").GetComponent<BattleHistory>().AddLog(unitName + " takes " + damage.ToString("N1") + " damage");
    }

    //updates the UI for which status effects are currently affecting this unit
    public override void UpdateStatusEffects()
    {
        //clear currently displayed effects
        foreach (GameObject go in statusEffectIcons)
        {
            Destroy(go);
        }
        statusEffectIcons.Clear();

        //display updated effects
        float ypos = -0.5f;
        var statusEffects = GetComponentsInChildren<StatusEffect>();
        foreach (StatusEffect effect in statusEffects)
        {
            var newIcon = Instantiate(statusEffectIconPrefab, this.gameObject.transform);
            statusEffectIcons.Add(newIcon);
            newIcon.transform.localPosition = new Vector3(-0.8f, ypos, 0);
            newIcon.GetComponent<SpriteRenderer>().sprite = effect.iconImage;
            newIcon.GetComponent<StatusEffectIcon>().statusEffect = effect;
            ypos += 0.4f;
        }
    }

    //display info
    void OnMouseOver()
    {
        if (activeHoverDisplay == null) //rdt - I think this no longer works since there is no collider on UnitStats gameobject
        {
            activeHoverDisplay = Instantiate(GameObject.Find("GameManager").GetComponent<UIManager>().enemyHoverInfoPrefab, GameObject.Find("HUDCanvas").transform);
            //activeHoverDisplay.transform.position = new Vector3(transform.position.x, transform.position.y + 2);
            activeHoverDisplay.GetComponent<RectTransform>().anchoredPosition = new Vector3(-70, 55);
            activeHoverDisplay.GetComponent<EnemyHoverInfo>().SetInfo(this.gameObject);
        }
    }

    //hide info
    void OnMouseExit()
    {
        Destroy(activeHoverDisplay);
    }
}
