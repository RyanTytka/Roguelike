using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyMenu : MonoBehaviour
{
    public GameObject[] playerButtons; 
    public GameObject[] equippedButtons;
    public GameObject[] abilityButtons;

    public GameObject displayStats;
    public GameObject inventoryItem;
    public void Close()
    {
        Destroy(this.gameObject);
    }

    public void Display(List<GameObject> party)
    {
        //show character portraits in buttons
        for (int i = 0; i < 4; i++)
        {
            try
            {
                GameObject character = party[i];
                playerButtons[i].GetComponent<Image>().sprite = character.GetComponent<SpriteRenderer>().sprite;
                playerButtons[i].GetComponent<Button>().onClick.AddListener( delegate
                {
                    displayStats.GetComponent<DisplayStats>().SetStats(character);
                    //show player abilities
                    List<GameObject> abilities = character.GetComponent<PlayerAbilities>().abilities;
                    int count = 0;
                    while (count < abilities.Count)
                    {
                        abilityButtons[count].GetComponent<Image>().sprite = abilities[count].GetComponent<AbilityInterface>().image;
                        abilityButtons[count].SetActive(true);
                        count++;
                    }
                    while(count < 4)
                    {
                        abilityButtons[count].SetActive(false);
                        count++;
                    }
                });
                playerButtons[i].SetActive(true);
            }
            catch
            {
                playerButtons[i].SetActive(false);
            }
        }
        //show inventory
        List<GameObject> inventory = GameObject.Find("ItemManager").GetComponent<ItemManager>().inventory;
        for (int i = 0; i < inventory.Count; i++)
        {
            GameObject item = Instantiate(inventoryItem, transform.GetChild(0));
            item.GetComponent<RectTransform>().anchoredPosition = new Vector3(-110 + 60 * i, 97, 0);
            item.GetComponent<Image>().sprite = inventory[i].GetComponent<ItemInterface>().image;
        }
        //select first character
        displayStats.GetComponent<DisplayStats>().SetStats(party[0]);
        List<GameObject> initialAbilities = party[0].GetComponent<PlayerAbilities>().abilities;
        int initialCount = 0;
        while (initialCount < initialAbilities.Count)
        {
            abilityButtons[initialCount].GetComponent<Image>().sprite = initialAbilities[initialCount].GetComponent<AbilityInterface>().image;
            abilityButtons[initialCount].SetActive(true);
            initialCount++;
        }
        while (initialCount < 4)
        {
            abilityButtons[initialCount].SetActive(false);
            initialCount++;
        }
    }
}
