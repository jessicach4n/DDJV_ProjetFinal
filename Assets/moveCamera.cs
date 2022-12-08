using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour
{
    private float maxDistance;
    public LayerMask masqueRaycast;
    // Start is called before the first frame update
    //faire un clamp : x de camera = x de joueur dans les positoin souhaitables
    void Start()
    {
        maxDistance = 500.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
 
    } 
}
