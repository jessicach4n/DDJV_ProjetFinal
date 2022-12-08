using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{

    //public GameObject projectile;
    //private mouvementRadish scriptPlayer;
    //public GameObject player;
    private Rigidbody2D rig;
    public float speed;
    public int direction;
    
    // Start is called before the first frame update
    void Start()
    {
        //speed = 5.0f;
        //Physics.IgnoreCollision(player.GetComponent<BoxCollider2D>(), GetComponent<Collider>());

        //scriptPlayer = player.GetComponent<mouvementRadish>();

        rig = GetComponent<Rigidbody2D>();

        Debug.Log("this is this.direction :" + this.direction);
        Debug.Log(rig.transform.right);

        rig.AddForce(transform.right * speed * this.direction, ForceMode2D.Impulse );
        //rig.AddForceAtPosition(transform.right * speed, Vector2.up * -100000 ,ForceMode2D.Impulse);

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        //transform.position = transform.position + transform.right * Time.fixedDeltaTime * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
    public void Shoot(int direction)
    {
         this.direction = direction;
        Debug.Log("this is direction shot  :" + this.direction);
        //transform.Rotate(new Vector3(0.0f, 0.0f, -1.0f), 90.0f * direction);
        //rig.velocity = rig.transform.right *speed;
        //rig = GetComponent<Rigidbody2D>();
        //Debug.Log(rig.transform.right);
        //StartCoroutine("Cwait");

        //Debug.Log("this is velocity :" + rig.velocity);
    }

}
