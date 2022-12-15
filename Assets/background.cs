using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class background : MonoBehaviour
{
    private Tilemap tileMap;
    // Start is called before the first frame update
    void Start()
    {
        tileMap = GameObject.Find("Background").GetComponent("Tilemap") as Tilemap;
        tileMap.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
