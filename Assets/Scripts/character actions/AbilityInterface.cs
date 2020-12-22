using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityInterface : MonoBehaviour
{
    public enum AbilityType { ATTACK, DEFENSE, UTILITY }

    public List<GameObject> targets;
    public GameObject caster;
    protected PlayerStats playerStats;

    public float manaCost;
    public AbilityType abilityType;
    public string abilityName, description;
    public Sprite image;


    void Start()
    {
        //playerStats = caster.GetComponent<PlayerStats>();
    }

    public void SetTargets(List<GameObject> targets)
    {
        this.targets = targets;
    }
    
    public abstract void Use();

}
