using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStats : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetStats(GameObject player)
    {
        PlayerStats statScript = player.GetComponent<PlayerStats>();
        GetComponent<Slider>().value = statScript.defense;
    }

}
