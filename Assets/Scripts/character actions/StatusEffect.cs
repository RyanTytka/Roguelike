using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusType { VULNERABLE }

public class StatusEffect : MonoBehaviour
{
    //list of info for each status effect 
    public List<string> names;
    public List<Sprite> icons; 
    public List<string> descriptions; 

    //this instance of a status effect's info
    public string statusName;
    public StatusType type; //which status effect this is
    public int duration; //how many turns this will last
    public int tier; //1, 2, or 3
    public string tierName; //I, II, or III
    public float tierPercent; //0.25, 0.5, or 0.75
    public Sprite iconImage;
    public string description;

    /// <summary>
    /// lower the duration and delete if duration is 0
    /// </summary>
    public void Progress()
    {
        duration--;
        if(duration <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
