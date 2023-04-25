using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityInfo : MonoBehaviour
{
    public GameObject nameObj, descriptionObj, manaObj, imageObj;

    //takes ability object and sets this display's info to that ability's info
    public void SetInfo(GameObject ability)
    {
        AbilityInterface abilityScript = ability.GetComponent<AbilityInterface>();
        nameObj.GetComponent<Text>().text = abilityScript.abilityName;
        descriptionObj.GetComponent<Text>().text = abilityScript.GetDescription();
        manaObj.GetComponent<Text>().text = abilityScript.manaCost.ToString();
        imageObj.GetComponent<Image>().sprite = abilityScript.image;
    }
}
