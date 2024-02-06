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
    string[] tierNames = new string[] { "I", "II", "III" };

    public void SetTargets(List<GameObject> targets)
    {
        this.targets = targets;
    }

    public abstract void Use();
    public abstract string GetDescription();

    public GameObject CreateStatusEffect(StatusType type, int tier, int duration, int iconID, GameObject parent)
    {
        GameObject obj = Instantiate(statusEffect, parent.transform);
        StatusEffect se = obj.GetComponent<StatusEffect>();
        se.type = type;
        se.tier = tier;
        se.tierName = tierNames[tier];
        se.duration = duration;
        se.tierPercent = tier * 0.25f;
        se.statusName = se.names[iconID];
        se.iconImage = se.icons[iconID];
        string d = se.descriptions[iconID];
        d = d.Replace("_X", ((int)(se.tierPercent * 100)).ToString());
        obj.GetComponent<StatusEffect>().description = d;
        return obj;
    }

}
