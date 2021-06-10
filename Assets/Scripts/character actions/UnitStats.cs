using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitStats : ActingUnit, IComparable
{
    public float maxHealth;
    public float currentHealth;
    public float mana;
    public float attack;
    public float magic;
    public float defense;
    public float resilience;
    public float speed;

    private bool dead = false;

    public GameObject statusEffectIconPrefab; //instantiated to show what statuses are affecting this unit
    private List<GameObject> statusEffectIcons = new List<GameObject>(); //keeps track of the objects created to show current effects

    //get stats that take status effects into account
    public float MaxHealth { get { return maxHealth * StatusEffectMods()[0]; } }
    public float Attack { get { return attack * StatusEffectMods()[3]; } }
    public float Magic { get { return magic * StatusEffectMods()[4]; } }
    public float Defense { get { return defense * StatusEffectMods()[5]; } }
    public float Resilience { get { return resilience * StatusEffectMods()[6]; } }
    public float Speed { get { return speed * StatusEffectMods()[7]; } }

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

    //adds to turn timer and returns true if their turn timer has been filled
    public override bool UpdateTurn()
    {
        turnTimer += speed;
        if (turnTimer >= 100)
        {
            turnTimer -= 100;
            return true;
        }
        return false;
    }

    public override void MyTurn()
    {
        //Debug.Log("Enemy Turn");
        
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
    }

    public void TakeDamage(float damage, int type)
    {
        //status effects
        var statusEffects = GetComponentsInChildren<StatusEffect>();
        //multiply damage for each status effect
        float multiplier = 1;
        foreach (StatusEffect se in statusEffects)
        {
            if (se.type == StatusType.VULNERABLE)
            {
                multiplier += se.tierPercent;
            }
        }
        damage *= multiplier;

        //defense/resilience
        if (type == 1)
        {
            damage *= 10 / (10 + Defense);
        }
        else if (type == 2)
        {
            damage *= 10 / (10 + Resilience);
        }

        print(gameObject.name + " took " + damage + " damage");
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
            dead = true;
            GameObject.Find("GameManager").GetComponent<BattleManager>().battlingUnits.Remove(this.gameObject);
            GameObject.Find("TurnTracker").GetComponent<TurnTracker>().UnitDied(this.gameObject);
            GetComponent<SpriteRenderer>().color = Color.gray;
            Destroy(this.gameObject);
        }
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
}
