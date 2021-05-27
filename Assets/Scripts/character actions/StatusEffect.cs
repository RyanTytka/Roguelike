using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusType { VULNERABLE }

public class StatusEffect : MonoBehaviour
{
    public StatusType type; //which status effect this is
    public int duration; //how many turns this will last
    public int tierName; //I, II, or III
    public float tierPercent; //0.25, 0.5, or 0.75

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
