using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour
{
    public GameObject player;
    public float minValue;
    public float maxValue;
    private float clamp;
    //faire un clamp : x de camera = x de joueur dans les positoin souhaitables
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        clamp = Mathf.Clamp(player.transform.position.x, minValue, maxValue);
        transform.position = new Vector3(clamp, transform.position.y, transform.position.z);
    } 
}
