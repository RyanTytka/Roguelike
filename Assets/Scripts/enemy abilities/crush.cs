using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crush : AbilityInterface
{
    public override void Use()
    {
        Debug.Log("crush used");

        //pick random player to attack
        targets.Clear();
        int playerNum = Random.Range(0, GameObject.Find("PlayerParty").transform.childCount);
        targets.Add(GameObject.Find("PlayerParty").transform.GetChild(playerNum).gameObject);

        foreach (GameObject obj in targets)
        {
            //deal damage
            obj.GetComponent<PlayerStats>().TakeDamage(3);
            CreateStatusEffect(StatusType.VULNERABLE, 2, 2, obj);
        }
    }
}