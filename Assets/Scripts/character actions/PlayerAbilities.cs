using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour
{
    public List<GameObject> abilities = new List<GameObject>();
    public GameObject buttonPrefab;

    void Start()
    {
        
    }

    public void LearnAbility(GameObject ability)
    {
        abilities.Add(ability);
    }

    //create buttons for user to use abilities
    public void Display()
    {
        GameObject menu = GameObject.Find("ActionMenu");
        for(int i = 0; i < abilities.Count; i++)
        {
            GameObject button = Instantiate(buttonPrefab, new Vector3(-8 + i * 3, -3, 0), Quaternion.identity, menu.transform);
            GameObject ability = abilities[i];
            button.GetComponent<Image>().sprite = ability.GetComponent<AbilityInterface>().image;
            button.GetComponent<Button>().onClick.AddListener(delegate
            {
                ability.GetComponent<AbilityInterface>().Use();
                GameObject.Find("GameManager").GetComponent<BattleManager>().TurnEnded();
            });
        }
    }
}
