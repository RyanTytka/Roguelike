using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusType {  STRENGTH_UP = 0, STRENGTH_DOWN, MAGIC_UP, MAGIC_DOWN, MANAREGEN_UP, MANAREGEN_DOWN, //stats
                        ARMOR_UP, ARMOR_DOWN, RES_UP, RES_DOWN, SPEED_UP, SPEED_DOWN,
                        POISONED, BLEEDING, STUNNED, CONFUSED, BURNING, //misc effects
                        MARKED, SHACKLED, DOOM } //enemy specific

public class StatusEffect : MonoBehaviour
{
    //list of info for each status effect 
    public List<string> names;
    public List<Sprite> icons; 
    public List<string> descriptions; 
    public List<bool> TicksDown; // Does this status effect remove 1 stack each round

    //this instance of a status effect's info
    public string statusName;
    public StatusType type; //which status effect this is
    public int stacks; //how many stacks of this effect you have
    public Sprite iconImage;
    public string description;

    /// <summary>
    /// lower the duration and delete if duration is 0
    /// </summary>
    public void Progress()
    {
        if(TicksDown[type])
        {
            stacks--;
            if(stacks <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
