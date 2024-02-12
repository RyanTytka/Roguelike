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

    public GameObject CreateStatusEffect(StatusType type, int stacks, int iconID, GameObject parent)
    {
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

}
