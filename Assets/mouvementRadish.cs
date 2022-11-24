using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouvementRadish : MonoBehaviour
{
    public float speed = 5.0f;
    public Animator anim;
    private Vector3 mouvement;
    private Vector3 dernierMouvement;
    private Rigidbody2D rig;
    public float jumpforce = 10f;
    // Start is called before the first frame update
    void Start()
    {
        mouvement.z = 0.0f;
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mouvement.x = Input.GetAxisRaw("Horizontal");
        mouvement.y = 0;

        if (mouvement.sqrMagnitude > 0.001f)
        {
            dernierMouvement = mouvement;
        }

        anim.SetFloat("Speed", mouvement.sqrMagnitude);
        anim.SetFloat("Horizontal", mouvement.x);
        anim.SetFloat("IdleDirection", GetDirection());

        //if (Input.GetButtonDown("Jump"))
        //{
        //    rig.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        //    Debug.Log("space was pressed");
        //}

        if (Input.GetKeyDown("space"))
        {
            rig.AddForce(Vector3.up * jumpforce, ForceMode2D.Impulse);
            Debug.Log("space was pressed");
            //DON'T DESTROY ON LOAD 
            anim.SetBool("Jump", true);
        }


    }

    private void FixedUpdate()
    {

       

        transform.position = transform.position + mouvement.normalized * Time.fixedDeltaTime * speed;
        rig.velocity = mouvement.normalized * speed;

        if (anim.GetBool("Jump"))
        {
            StartCoroutine(CJump());
        }
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
            anim.SetBool("Jump", false);
            Debug.Log("wall was hit");
        }
    }

    IEnumerator CJump()
    {
        yield return new WaitForSeconds(0.2f);
        rig.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.8f);
        rig.AddForce(Vector2.down * jumpforce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.8f);
    }
}
