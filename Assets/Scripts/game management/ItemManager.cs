using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<GameObject> inventory; //items currently in possession of party
    public List<GameObject> allArtifacts, allWeapons, allArmor; //all items in the game
}
