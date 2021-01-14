using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DisplayStats : MonoBehaviour
{
    GameObject playerReference; //regerence to the player this is displaying

    public bool ShowModButtons;
    public GameObject modButtons; //+ and - buttons to edit stats
    
    int currentStat;    //which stat the mod buttons are currently on
    public int modPointsLeft;  //how many points can be put into modding a characters stats
    public int[] modChanges;   //tracks which mods have been modified

    public Slider[] sliders;
    public Text[] labels;

    public Text levelText;
    public Slider xpSlider;

    public Image portrait;
    public bool showPortrait = false;

    // Start is called before the first frame update
    void Start()
    {
        //modButtons = GameObject.Find("ButtonModGroup");
        modChanges = new int[8];
        //ShowModButtons = false;
        HideMods();
    }

    public void HideMods()
    {
        //ShowModButtons = false;
        modButtons.SetActive(false);
    }

    public void ShowMods()
    {
        ShowModButtons = true;
        modButtons.SetActive(true);
    }

    //sets slider and text values to player's stats
    public void SetStats(GameObject player)
    {
        //get highest stat for sliders
        playerReference = player;
        PlayerStats statScript = player.GetComponent<PlayerStats>();
        float maxStat = statScript.MaxHealth;
        if(statScript.MaxMana > maxStat)
            maxStat = statScript.MaxMana;
        if (statScript.Attack > maxStat)
            maxStat = statScript.Attack;
        if (statScript.Magic > maxStat)
            maxStat = statScript.Magic;
        if (statScript.Defense > maxStat)
            maxStat = statScript.Defense;
        if (statScript.Resilience > maxStat)
            maxStat = statScript.Resilience;
        if (statScript.Speed > maxStat)
            maxStat = statScript.Speed;

        //set text and slider labels
        labels[0].text = statScript.MaxHealth.ToString();
        sliders[0].maxValue = maxStat;
        sliders[0].value = statScript.MaxHealth;

        labels[1].text = statScript.MaxMana.ToString();
        sliders[1].maxValue = maxStat;
        sliders[1].value = statScript.MaxMana;

        labels[2].text = statScript.ManaRegen.ToString();
        sliders[2].maxValue = maxStat;
        sliders[2].value = statScript.ManaRegen;

        labels[3].text = statScript.Attack.ToString();
        sliders[3].maxValue = maxStat;
        sliders[3].value = statScript.Attack;

        labels[4].text = statScript.Magic.ToString();
        sliders[4].maxValue = maxStat;
        sliders[4].value = statScript.Magic;

        labels[5].text = statScript.Defense.ToString();
        sliders[5].maxValue = maxStat;
        sliders[5].value = statScript.Defense;

        labels[6].text = statScript.Resilience.ToString();
        sliders[6].maxValue = maxStat;
        sliders[6].value = statScript.Resilience;

        labels[7].text = statScript.Speed.ToString();
        sliders[7].maxValue = maxStat;
        sliders[7].value = statScript.Speed;

        levelText.text = "Level " + statScript.level;
        xpSlider.value = statScript.xp;

        if (ShowModButtons)
            labels[8].text = "Upgrade Points: " + modPointsLeft;
        else
            labels[8].text = "";
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //if showModButtons is true, set the buttons to the corerct position
        if (ShowModButtons)
        {
            //Debug.Log("Mouse Pos: " + mousePos.x + "," + mousePos.y);
            if (Mathf.Abs(mousePos.x - .6f - transform.position.x) < 2.1)
            {
                //find nearest slider to set y position
                //Slider[] sliders = GetComponentsInChildren<Slider>();
                float yPos = -999;
                for(int i = 0; i < sliders.Length; i++)
                {
                    if (Mathf.Abs((mousePos.y + -.1f) - sliders[i].transform.position.y) < .275)
                    {
                        yPos = sliders[i].transform.position.y;
                        currentStat = i;
                    }
                }
                if (yPos == -999)
                    HideMods();
                //show buttons and set their position
                ShowMods();
                modButtons.transform.position = new Vector3(transform.position.x, yPos, 0);
            }
            else
            {
                HideMods();
            }
        }
        portrait.gameObject.SetActive(showPortrait);
    }

    //when + mod button is clicked
    public void AddToStat()
    {
        PlayerStats stats = playerReference.GetComponent<PlayerStats>();
        if (modPointsLeft > 0)
        {
            if (currentStat == 0)
                stats.maxHealth += 2;
            if (currentStat == 1)
                stats.maxMana += 2;
            if (currentStat == 2)
                stats.manaRegen += 0.5f;
            if (currentStat == 3)
                stats.attack += 1;
            if (currentStat == 4)
                stats.magic += 1;
            if (currentStat == 5)
                stats.defense += 1;
            if (currentStat == 6)
                stats.resilience += 1;
            if (currentStat == 7)
                stats.speed += 2;
            modChanges[currentStat]++;
            modPointsLeft--;
        }
        SetStats(playerReference);
    }

    //when - mod button is clicked
    public void SubtractFromStat()
    {
        if (modChanges[currentStat] == 0)
            return;

        modPointsLeft++;
        PlayerStats stats = playerReference.GetComponent<PlayerStats>();
        
        if (currentStat == 0)
            stats.maxHealth -= 2;
        if (currentStat == 1)
            stats.maxMana -= 2;
        if (currentStat == 2)
            stats.manaRegen -= 0.5f;
        if (currentStat == 3)
            stats.attack -= 1;
        if (currentStat == 4)
            stats.magic -= 1;
        if (currentStat == 5)
            stats.defense -= 1;
        if (currentStat == 6)
            stats.resilience -= 1;
        if (currentStat == 7)
            stats.speed -= 2;
        
        modChanges[currentStat]--;
        SetStats(playerReference);
    }




}
