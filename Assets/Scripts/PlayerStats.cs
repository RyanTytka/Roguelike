using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public float health;
    public float mana;
    public float attack;
    public float magic;
    public float defense;
    public float resilience;
    public float speed;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        gameObject.SetActive(false);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameObject.SetActive(scene.name == "Battle");
    }

    public void RandomizeStats()
    {
        health += Random.Range(-2,4);
        mana += Random.Range(-2,3);
        attack += Random.Range(-1,2);
        magic += Random.Range(-1,2);
        defense += Random.Range(-2,3);
        resilience += Random.Range(-2,3);
        speed += Random.Range(-2,3);

        health = Mathf.Max(0, health);
        mana = Mathf.Max(0, mana);
        attack = Mathf.Max(0, attack);
        magic = Mathf.Max(0, magic);
        defense = Mathf.Max(0, defense);
        resilience = Mathf.Max(0, resilience);
        speed = Mathf.Max(0, speed);
    }

}
