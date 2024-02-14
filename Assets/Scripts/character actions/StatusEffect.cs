using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{
    //list of info for each status effect 
    public List<string> names;
    public List<Sprite> icons; 
    public List<string> descriptions; 
    public List<bool> TicksDown; // Does this status effect remove 1 stack each round
    public List<bool> IsGood; // Is this a buff or debuff? 

    //this instance of a status effect's info
    public string statusName;
    public StatusTypeEnum type; //which status effect this is
    public int stacks; //how many stacks of this effect you have
    public Sprite iconImage;
    public string description;

    /// <summary>
    /// lower the duration and delete if duration is 0
    /// </summary>
    public void Progress()
    {
        if(TicksDown[(int)type])
        {
            stacks--;
            if(stacks <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
