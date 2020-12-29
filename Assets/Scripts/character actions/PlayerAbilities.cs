using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour
{
    public GameObject caster;
    public List<GameObject> abilities = new List<GameObject>();
    public GameObject buttonPrefab;

    private List<GameObject> activeButtons = new List<GameObject>();

    void Start()
    {
        
    }

    public void LearnAbility(GameObject ability)
    {
        ability.GetComponent<AbilityInterface>().caster = caster;
        abilities.Add(ability);
    }

    //create buttons for user to use abilities
    public void Display()
    {
        GameObject menu = GameObject.Find("ActionMenu");
        for(int i = 0; i < abilities.Count; i++)
        {
            GameObject button = Instantiate(buttonPrefab, new Vector3(-6.0f + i * 1.5f, -3.5f, 0), Quaternion.identity, menu.transform);
            GameObject ability = abilities[i];
            button.GetComponent<AbilityButton>().SetReference(gameObject, ability);
            activeButtons.Add(button);
            button.GetComponent<Image>().sprite = ability.GetComponent<AbilityInterface>().image;
            button.GetComponent<Button>().onClick.AddListener(delegate
            {
                if(GetComponent<PlayerStats>().currentMana >= ability.GetComponent<AbilityInterface>().manaCost)
                    ability.GetComponent<AbilityInterface>().selected = true;
            });
        }
    }

    public void Hide()
    {
        foreach(GameObject go in activeButtons)
        {
            Destroy(go);
        }
    }
}
