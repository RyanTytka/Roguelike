using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doomBlade : AbilityInterface
{
    public override void Use()
    {
        //pick random player to attack
        targets.Clear();
        int playerNum = Random.Range(0, GameObject.Find("PlayerParty").transform.childCount);
        targets.Add(GameObject.Find("PlayerParty").transform.GetChild(playerNum).gameObject);

        foreach (GameObject obj in targets)
        {
            //see if I have any doom stacks
            var statusEffects = GetComponentsInChildren<StatusEffect>();
            int stacks = 0;
            foreach(StatusEffect se in statusEffects)
            {
                if(se.type == StatusType.DOOM)
                {
                    stacks = se.stacks;
                }
            }
            //deal damage
            obj.GetComponent<PlayerStats>().TakeDamage(caster.GetComponent<UnitStats>().Attack * 0.4f * 2 ^ stacks, 1);
        }
    }

    public override string GetDescription()
    {
        return "Slice an enemy, sealing their doom. Double the damage for each stack of Doom on you.";
    }
}
