using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouvementBunny : MonoBehaviour
{
    public float speed = 5.0f;
    public Animator anim;
    private Vector3 mouvement;
    private Vector3 dernierMouvement;
    public GameObject projectile;
    private Rigidbody2D rig;
    public float jumpforce = 7.0f;
    public bool canJump = true;
    private bool canShoot = true;
    public float rateOfFireInSeconds = 1.0f;
    public float rateOfJump = 3.0f;
    public GameObject EFX;
    void Start()
    {
        mouvement.z = 0.0f;
        rig = GetComponent<Rigidbody2D>();
        anim.SetFloat("DernierHorizontal", 1);
        anim.SetFloat("IdleDirection", 1);

        if (gameObject.tag == "BunnyKiller")
        {
            StartCoroutine(CShoot());
        }

    }

    void Update()
    {
        if (gameObject.tag == "BunnyLevels" )
        {
            StartCoroutine(CShoot());
        }
        else if (gameObject.tag == "BunnyMainMenu" || gameObject.tag == "BunnyKiller")
        {
            StartCoroutine(CJump());
        }
        else if (gameObject.tag == "BunnyEndScene")
        {
            StartCoroutine(CSuicide());
        }

        mouvement.y = 0.0f;
        if (mouvement.sqrMagnitude > 0.001f)
        {

            dernierMouvement = mouvement;
            anim.SetFloat("DernierHorizontal", GetDirection());
        }

        anim.SetFloat("Speed", mouvement.sqrMagnitude);
        anim.SetFloat("Horizontal", mouvement.x);

    }

    private void FixedUpdate()
    {
        rig.velocity = new Vector2(mouvement.normalized.x * speed, rig.velocity.y);
    }

    public int GetDirection()
    {
        int direction = 0;
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
            anim.SetBool("Jump", false);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            StartCoroutine(CDeath());
        }
    }

    IEnumerator CDeath()
    {
        Instantiate(EFX, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }
    IEnumerator CJump()
    {
        if (canJump)
        {   
            rig.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
            anim.SetBool("Jump", true);
            canJump = false;
            yield return new WaitForSeconds(rateOfJump);
            anim.SetFloat("DernierHorizontal", anim.GetFloat("DernierHorizontal") * -1);
            anim.SetFloat("IdleDirection", anim.GetFloat("IdleDirection") *-1);
            
            canJump = !canJump;
        }
    }
    IEnumerator CShoot()
    {
        if (canShoot)
        {
            canShoot = !canShoot;
            float angle = GetDirection() == -1 ? 180.0f : 0.0f;
            Quaternion initalRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            GameObject inst = Instantiate(projectile, transform.position, initalRotation);
            yield return new WaitForSeconds(rateOfFireInSeconds);
            canShoot = !canShoot;
        }
    }
    IEnumerator CSuicide()
    {
        mouvement.x = 1;
        yield return new WaitForSeconds(0.5f);
        rig.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        anim.SetBool("Jump", true);
        jumpforce = 0;
        yield return new WaitForSeconds(1.5f);
     
    }
}
