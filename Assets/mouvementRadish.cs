using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public int nbLives = 4;
    public int nbPiesCollected = 0;
    public int maxPies = 5;
    public GameObject startingPoint;
    public GameObject poof;
    private bool canMove;

    void Start()
    {
        mouvement.z = 0.0f;
        rig = GetComponent<Rigidbody2D>();
        canMove = gameObject.tag == "LastRadish" ? false : true;
    }

    void Update()
    {
        if (canMove)
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
                    anim.SetBool("Jump", true);
                    canJump = false;
                }
            }

            if (Input.GetKeyDown("x"))
            {
                //? la place, je tourne l'objet directement ? l'instantiation... ?a fonctionne mais ?a n'explique pas pourquoi ?a plante notre ancienne m?thode. -MAL
                float angle = GetDirection() == -1 ? 180.0f : 0.0f;
                Quaternion initalRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                GameObject inst = Instantiate(projectile, transform.position, initalRotation);
            }
        }
        
    }

    private void FixedUpdate()
    {
        rig.velocity = new Vector2(mouvement.normalized.x * speed, rig.velocity.y) ;
    }

    public int GetDirection()
    {
        int direction =0;
        float petiteValeur = 0.001f;
        if (dernierMouvement.x < -petiteValeur)
        {
            direction = -1;
        }

        else
        {
            direction = 1;
        }

        return direction;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Vector2 norme = collision.GetContact(0).normal;
            float produitScalaire = Vector2.Dot(norme, Vector2.up);
            if (produitScalaire > 0.9f)
            {
                anim.SetBool("Jump", false);
                canJump = true;
                Instantiate(poof, transform.position, Quaternion.identity);
            }

        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Reload();
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyProjectile"))
        {
            if(gameObject.tag == "LastRadish")
             {
                Destroy(this.gameObject);
            }
            else
            {
                Reload();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pie"))
        {
            Destroy(collision.gameObject);

            nbPiesCollected++;
            Debug.Log(nbPiesCollected);
        }
    }

    public void OnHitLazer()
    {
        Reload();
    }

    public void Reload()
    {
        nbLives--;
        if (nbLives >= 1)
        {
            gameObject.transform.position = startingPoint.transform.position;
        }
        else
        {
            SceneManager.LoadScene("GameLostScene");
        }
    }
}
