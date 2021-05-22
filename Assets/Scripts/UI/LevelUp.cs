using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUp : MonoBehaviour
{
    public GameObject statsObj, continueButtonObj;
    public GameObject newAbilitySelect;
    public List<GameObject> toLevelUp;

    // Start is called before the first frame update
    void Start()
    {
        PartyManager pm = GameObject.Find("PlayerParty").GetComponent<PartyManager>();

        //get list of who needs to level up
        toLevelUp = new List<GameObject>();
        foreach (GameObject player in pm.party)
        {
            int num = player.GetComponent<PlayerStats>().levelUps;
            for (int i = 0; i < num; i++)
                toLevelUp.Add(player);
        }
        LoadLevelUp(toLevelUp, 0);
    }

    public void LoadLevelUp(List<GameObject> players, int index)
    {
        GameObject player = players[index];
        player.GetComponent<PlayerStats>().levelUps--;
        statsObj.SetActive(true);
        continueButtonObj.SetActive(true);

        statsObj.GetComponent<DisplayStats>().modPointsLeft = 3;
        statsObj.GetComponent<DisplayStats>().ShowMods();
        statsObj.GetComponent<DisplayStats>().showPortrait = true;
        statsObj.GetComponent<DisplayStats>().portrait.sprite = player.GetComponent<SpriteRenderer>().sprite;
        statsObj.GetComponent<DisplayStats>().SetStats(player);

        continueButtonObj.GetComponent<Button>().onClick.AddListener(delegate
        {
            statsObj.SetActive(false);
            continueButtonObj.SetActive(false);
            GameObject.Find("ChooseText").GetComponent<Text>().text = "Learn a new ability";

            //display new ability to learn
            GameObject abilitySelect = Instantiate(newAbilitySelect, new Vector3(0, 1, 0), Quaternion.identity, GameObject.Find("HUDCanvas").transform);
            abilityManager abilityManager = GameObject.Find("AbilityManager").GetComponent<abilityManager>();

            List<GameObject> choices = abilityManager.newAbility();
            for (int i = 0; i < 3; i++)
            {
                GameObject choice = Instantiate(choices[i], new Vector3(0, i * 3, 0), Quaternion.identity);
                NewAbilitySelect select = abilitySelect.GetComponent<NewAbilitySelect>();
                select.AddChoice(choice);
                PlayerAbilities playerAbilities = player.GetComponent<PlayerAbilities>();
                select.GetComponentsInChildren<Button>()[i].onClick.AddListener(delegate
                {
                    //learn ability and continue to map scene/next char level up
                    playerAbilities.LearnAbility(choice);
                    choice.transform.parent = playerAbilities.transform;
                    Destroy(abilitySelect);
                    //Destroy(choice);
                    if (index >= players.Count - 1)
                    {
                        SceneManager.LoadScene("Map");
                    }
                    else
                    {
                        LoadLevelUp(players, index + 1);
                    }
                });
            }

        });
    }

}
