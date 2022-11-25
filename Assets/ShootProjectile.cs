using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{

    public GameObject projectile;
    public mouvementRadish scriptPlayer;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("x"))
        {
            Debug.Log("fire was pressed");
        

            GameObject inst = Instantiate(projectile, transform.position, Quaternion.identity);

            inst.transform.Rotate(Vector3.forward, scriptPlayer.GetDirection() * 90);
        }
    }
    private void FixedUpdate()
    {
        transform.position = transform.position + transform.right * Time.fixedDeltaTime * speed;
    }
}
