using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : ActingUnit
{
    //base stats
    public float maxHealth;
    public float currentHealth;
    public float maxMana;
    public float currentMana;
    public float manaRegen;
    public float attack;
    public float magic;
    public float defense;
    public float resilience;
    public float speed;

    public int level = 1;
    public float xp = 0;
    public int levelUps;

    public GameObject statusEffectIconPrefab; //instantiated to show what statuses are affecting this player
    private List<GameObject> statusEffectIcons = new List<GameObject>(); //keeps track of the objects created to show current effects

    //get stats that take items into account
    public float MaxHealth { get { return (maxHealth + GetComponent<PlayerItems>().StatMods()[0]) * StatusEffectMods()[0]; } }
    public float MaxMana { get { return (maxMana + GetComponent<PlayerItems>().StatMods()[1]) * StatusEffectMods()[1]; } }
    public float ManaRegen { get { return (manaRegen + GetComponent<PlayerItems>().StatMods()[2]) * StatusEffectMods()[2]; } }
    public float Attack { get { return (attack + GetComponent<PlayerItems>().StatMods()[3]) * StatusEffectMods()[3]; } }
    public float Magic { get { return (magic + GetComponent<PlayerItems>().StatMods()[4]) * StatusEffectMods()[4]; } }
    public float Defense { get { return (defense + GetComponent<PlayerItems>().StatMods()[5]) * StatusEffectMods()[5]; } }
    public float Resilience { get { return (resilience + GetComponent<PlayerItems>().StatMods()[6]) * StatusEffectMods()[6]; } }
    public float Speed { get { return (speed + GetComponent<PlayerItems>().StatMods()[7]) * StatusEffectMods()[7]; } }

    public void RandomizeStats()
    {
        gameObject.SetActive(false);
        //add to a random stat 12 times
        for (int i = 0; i < 12; i++)
        {
            int stat = Random.Range(1, 9);
            if (stat == 1)
                maxHealth += Random.Range(1, 3);
            else if (stat == 2)
                maxMana += Random.Range(1, 3);
            else if (stat == 3)
                manaRegen += Random.Range(3, 8) / 10.0f;
            else if (stat == 4)
                attack += 1;
            else if (stat == 5)
                magic += 1;
            else if (stat == 6)
                defense += 1;
            else if (stat == 7)
                resilience += 1;
            else if (stat == 8)
                speed += Random.Range(1, 3);
        }
        currentHealth = maxHealth;
        currentMana = maxMana;
    }

    public void AddStats(int[] newStats)
    {
        maxHealth += newStats[0] * 2;
        currentHealth += newStats[0] * 2;
        maxMana += newStats[1] * 2;
        currentMana += newStats[1] * 2;
        manaRegen += newStats[2] * 0.5f;
        attack += newStats[3];
        magic += newStats[4];
        defense += newStats[5];
        resilience += newStats[6];
        speed += newStats[7] * 2;
    }

    //adds to turn timer and returns true if their turn timer has been filled
    public override bool UpdateTurn()
    {
        turnTimer += speed;
        if(turnTimer >= 100)
        {
            turnTimer -= 100;
            return true;
        }
        return false;
    }

    public override void MyTurn()
    {
        //Debug.Log("Player Turn");
        //highlight me
        GetComponent<SpriteRenderer>().color = Color.yellow;
        //display my moves
        PlayerAbilities abilities = GetComponent<PlayerAbilities>();
        abilities.Display();
        //mana regen
        currentMana = Mathf.Min(maxMana, currentMana + manaRegen);
        SetBars();
    }

    public override void EndTurn()
    {
        //unhighlight me
        GetComponent<SpriteRenderer>().color = Color.white;
        //remove my ability buttons
        PlayerAbilities abilities = GetComponent<PlayerAbilities>();
        abilities.Hide();
    }

    //update mana and health bar with current values
    public void SetBars()
    {
        GetComponentInChildren<HealthBar>().CurrentValue = currentHealth;
        GetComponentInChildren<HealthBar>().MaxValue = maxHealth;
        GetComponentInChildren<ManaBar>().CurrentValue = currentMana;
        GetComponentInChildren<ManaBar>().MaxValue = maxMana;
    }

    public void TakeDamage(float damage)
    {
        //status effects
        var statusEffects = GetComponentsInChildren<StatusEffect>();
        //multiply damage for each status effect
        float multiplier = 1; 
        foreach(StatusEffect se in statusEffects)
        {
            if(se.type == StatusType.VULNERABLE)
            {
                multiplier += se.tierPercent;
            }
        }
        damage *= multiplier;

        //armor/resilience
        // *not yet implemented*

        print(gameObject.name + " took " + damage + " damage");
        //apply damage
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        SetBars();
    }

    public void UseMana(float amount)
    {
        currentMana = Mathf.Max(currentMana - amount, 0);
        SetBars();
    }

    //adds xp to player, levels up if enough xp
    public void GainXP(float amount)
    {
        xp += amount;
        if(xp >= 10)
        {
            level++;
            xp -= 10;
            levelUps++;
            SceneManager.LoadScene("LevelUp");
        }
    }

    //updates the UI for which status effects are currently affecting this player
    public override void UpdateStatusEffects()
    {
        //clear currently displayed effects
        foreach(GameObject go in statusEffectIcons)
        {
            Destroy(go);
        }
        statusEffectIcons.Clear();

        //display updated effects
        float ypos = -0.5f;
        var statusEffects = GetComponentsInChildren<StatusEffect>();
        foreach(StatusEffect effect in statusEffects)
        {
            var newIcon = Instantiate(statusEffectIconPrefab, this.gameObject.transform);
            statusEffectIcons.Add(newIcon);
            newIcon.transform.localPosition = new Vector3(0.8f, ypos, 0);
            newIcon.GetComponent<SpriteRenderer>().sprite = effect.iconImage;
            newIcon.GetComponent<StatusEffectIcon>().statusEffect = effect;
            ypos += 0.4f;
        }
    }
}
