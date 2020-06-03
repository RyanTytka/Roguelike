using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public int seed;
    MapManager mapManager;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        seed = (int)Random.Range(0, 1000000);
        Random.seed = seed;
        mapManager = GetComponent<MapManager>();

        mapManager.CreateMap(seed);
    }

}
