using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slash : AbilityInterface
{
    public override void Use()
    {
        Debug.Log("slash used");

        //pick random player to attack
        targets.Clear();
        int playerNum = Random.Range(0, GameObject.Find("PlayerParty").transform.childCount);
        targets.Add(GameObject.Find("PlayerParty").transform.GetChild(playerNum).gameObject);

        foreach (GameObject obj in targets)
        {
            //deal damage
            obj.GetComponent<PlayerStats>().TakeDamage(caster.GetComponent<UnitStats>().Attack, 1);
        }
    }

    public override string GetDescription()
    {
        return "Slash an enemy.";
    }
}
