using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouvementRadish : MonoBehaviour
{
    public float speed = 5.0f;
    public Animator anim;
    private Vector3 mouvement;
    private Vector3 dernierMouvement;
    public GameObject projectile;
    private Rigidbody2D rig;
    public float jumpforce = 7.0f;
    public bool canJump = true;
    void Start()
    {
        mouvement.z = 0.0f;
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        mouvement.x = Input.GetAxisRaw("Horizontal");
        mouvement.y = 0.0f;

        if (mouvement.sqrMagnitude > 0.001f)
        {
            dernierMouvement = mouvement;
            anim.SetFloat("DernierHorizontal", GetDirection());
        }

        anim.SetFloat("Speed", mouvement.sqrMagnitude);
        anim.SetFloat("Horizontal", mouvement.x);
        anim.SetFloat("IdleDirection", GetDirection());

        

        if (Input.GetKeyDown("z"))
        {
            if (canJump)
            {
                rig.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
                Debug.Log("jump was pressed");
                anim.SetBool("Jump", true);
                canJump = false;
            }
        }

        if (Input.GetKeyDown("x"))
        {
            Debug.Log("fire was pressed");
            GameObject inst = Instantiate(projectile, transform.position, Quaternion.identity);

            int intDirection = GetDirection();
            ShootProjectile script = projectile.GetComponent<ShootProjectile>();
            script.Shoot(intDirection);
            //inst.transform.Rotate(Vector3.forward, scriptPlayer.GetDirection() * 90);
        }





    }

    private void FixedUpdate()
    {
        //transform.position = transform.position + mouvement.normalized * Time.fixedDeltaTime * speed;
        rig.velocity = new Vector2(mouvement.normalized.x * speed, rig.velocity.y) ;
        //rig.AddForce(mouvement.normalized * speed, ForceMode2D.Force);
    }

    public int GetDirection()
    {
        int direction = 0;
        float petiteValeur = 0.001f;
        if (dernierMouvement.x < -petiteValeur)
        {
            direction = 1;
        }
        
        else
        {
            direction = 0;
        }

        return direction;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Vector2 norme = collision.GetContact(0).normal;
            float produitScalaire = Vector2.Dot (norme,Vector2.up);
            if(produitScalaire > 0.9f)
            {
            anim.SetBool("Jump", false);
                canJump = true;
            }
            Debug.Log("wall was hit");
        }
    }
}
