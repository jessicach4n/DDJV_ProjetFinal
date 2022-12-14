using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportation : MonoBehaviour
{
    public GameObject player;
    public GameObject exit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player.transform.position = exit.transform.position;
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
