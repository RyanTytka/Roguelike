using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public List<Sprite> backgrounds;
    public GameObject defaultButton, defaultText;

    void Start()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Event")
        {
            Encounter encounter = GetComponentInChildren<Encounter>();
            switch(encounter.eventID)
            {
                case 2: //heal fountain
                    LoadBackground(backgrounds[0]);
                    //string s = "You find a fountain that glows with life.Your party takes a drink and feels rejuvinated.";
                    //GameObject t = CreateText(s, 0, 100, 400, 200, Color.white, 30);
                    GameObject b = CreateButton("Continue", 0, 0, 100, 30);
                    b.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        //heal party by half their max hp
                        List<GameObject> party = GameObject.Find("PlayerParty").GetComponent<PartyManager>().party;
                        foreach(GameObject go in party)
                        {
                            int mHP = (int)go.GetComponent<PlayerStats>().maxHealth;
                            int cHP = (int)go.GetComponent<PlayerStats>().currentHealth;
                            go.GetComponent<PlayerStats>().currentHealth = Mathf.Min(cHP + mHP / 2, mHP);
                        }
                        //go back to map
                        LoadScene("Map");
                    });
                    break;
            }
        }
    }

    private void LoadBackground(Sprite s)
    {
        GameObject.Find("Background").GetComponent<Image>().sprite = s;
    }

    //creates button object on canvas with text at (x,y), returns button object
    private GameObject CreateButton(string text, float xPos, float yPos, int width, int height)
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject b = Instantiate(defaultButton, canvas.transform);
        b.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
        b.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        b.GetComponentInChildren<Text>().text = text;
        return b;
    }

    //creates text object on canvas with text at (x,y) color, returns text object
    private GameObject CreateText(string text, float xPos, float yPos, int width, int height, Color color, int fontSize)
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject t = Instantiate(defaultText, canvas.transform);
        t.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
        t.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        t.GetComponent<Text>().color = color;
        t.GetComponent<Text>().text = text;
        t.GetComponent<Text>().fontSize = fontSize;
        return t;
    }

    private void LoadScene(string SceneName)
    {
        //update map
        try
        {
            GameObject manager = GameObject.Find("GameManager");
            Vector2 playerPos = manager.GetComponentInChildren<PlayerMovement>(true).GetPos();
            manager.GetComponent<MapManager>().RoomFinished(playerPos);
        }
        catch { }
        SceneManager.LoadScene(SceneName);
    }
}
