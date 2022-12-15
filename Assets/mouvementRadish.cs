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
    private bool maxPiesAchieved = false;
    public GameObject startingPoint;
    public GameObject poof;
    private bool canMove;
    private CapsuleCollider2D myCollider;
    public LayerMask wallCollider;

    void Start()
    {
        myCollider = GetComponent<CapsuleCollider2D>();
        mouvement.z = 0.0f;
        rig = GetComponent<Rigidbody2D>();
        canMove = gameObject.tag == "LastRadish" ? false : true;
    }

    void StartPiesEvent()
    {
        EventManager.TriggerEvent("MaxPies", gameObject.transform);
    }

    void StartBackgroundEvent()
    {
        EventManager.TriggerEvent("RedBackground", gameObject.transform);
    }

    void Update()
    {
        if (maxPiesAchieved)
        {
            StartPiesEvent();
        }
        if (canMove)
        {
            mouvement.x = Input.GetAxisRaw("Horizontal");
            mouvement.y = 0.0f;
            //Collider2D[] results; 
            Collider2D[] results = Physics2D.OverlapBoxAll(gameObject.transform.position, myCollider.size, Vector2.Angle(Vector2.zero, transform.position), wallCollider);
            foreach(var memeber in results)
            {
                if(memeber.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    //Debug.Log("hit floor");
                    //mouvement.x = 0;
                }
            }
                //if (wallhit != null)
                //{
                //    Debug.Log("wall was hit");
                //    //mouvement.x = 0;
                //}
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
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Debug.Log(collision.contactCount);
            for (int i = 0; i < collision.contactCount; i++)
            {
                Debug.Log(collision.transform.position);
                Vector2 norme = collision.GetContact(i).normal;
                float produitScalaire = Vector2.Dot(norme, Vector2.up);
                if (produitScalaire > 0.9f)
                {
                    anim.SetFloat("Speed", 0.0f);
                    anim.SetFloat("Horizontal", 0.0f);
                    break;
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Instantiate(poof, transform.position, Quaternion.identity);
            anim.SetBool("Jump", false);
            canJump = true;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
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
            maxPiesAchieved = nbPiesCollected == maxPies;
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
            StartBackgroundEvent();
            gameObject.transform.position = startingPoint.transform.position;
        }
        else
        {
            SceneManager.LoadScene("GameLostScene");
        }
    }
}
