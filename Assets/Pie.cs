using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pie : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject EFX;
    private GameObject tmp;
    void Start()
    {
        tmp = Instantiate(EFX, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(tmp);
    }

}
