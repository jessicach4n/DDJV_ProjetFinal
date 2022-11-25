using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{

    public GameObject projectile;
    public mouvementRadish scriptPlayer;
    public GameObject player;
    public Rigidbody2D rig;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = 5.0f;
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        transform.position = transform.position + transform.right * Time.fixedDeltaTime * speed;
    }

    public void Shoot(int direction)
    {
        rig = GetComponent<Rigidbody2D>();
        transform.Rotate(new Vector3(0.0f, 0.0f, -1.0f), 90.0f * direction);
        rig.velocity = rig.transform.right * speed;
    }
}
