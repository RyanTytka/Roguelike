using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityInterface : MonoBehaviour
{
    public enum AbilityType { ATTACK, DEFENSE, UTILITY }

    public List<GameObject> targets = new List<GameObject>();
    public GameObject caster;
    protected PlayerStats playerStats;

    public float manaCost;
    public AbilityType abilityType;
    public string abilityName, description;
    public Sprite image;

    public bool selected = false;

    public GameObject statusEffect; //blank status effect prefab

    void Start()
    {
        //playerStats = caster.GetComponent<PlayerStats>();
    }

    public void SetTargets(List<GameObject> targets)
    {
        this.targets = targets;
    }

    public abstract void Use();

    public GameObject CreateStatusEffect(StatusType type, int tier, int duration, GameObject parent)
    {
        GameObject obj = Instantiate(statusEffect, parent.transform);
        obj.GetComponent<StatusEffect>().type = type;
        obj.GetComponent<StatusEffect>().tierName = tier;
        obj.GetComponent<StatusEffect>().duration = duration;
        obj.GetComponent<StatusEffect>().tierPercent = tier * 0.25f;
        return obj;
    }
}
