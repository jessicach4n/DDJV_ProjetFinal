using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;


public class background : MonoBehaviour
{
    private Tilemap tileMap;
    private UnityAction<object> EventRedBackground;

    void Start()
    {
        tileMap = GameObject.Find("Background").GetComponent("Tilemap") as Tilemap;
    }

    private void Awake()
    {
        EventRedBackground = new UnityAction<object>(EventBackgroundReaction);
    }
    private void OnEnable()
    {
        EventManager.StartListening("RedBackground", EventRedBackground);
    }
    private void OnDisable()
    {
        EventManager.StopListening("RedBackground", EventRedBackground);
    }
    
    void EventBackgroundReaction(object data)
    {
        StartCoroutine(CBackgroundColorChange());
    }

    IEnumerator CBackgroundColorChange()
    {
        Color col = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        tileMap.color = col;
        for (float x = 0.0f; x < 1.0f; x += 0.1f)
        {
            col.b = x;
            col.g = x;
            tileMap.color = col;
            yield return new WaitForSeconds(.02f);
        }
        col.a = 1.0f;
        tileMap.color = col;
        tileMap.color = Color.white;
    }
}
