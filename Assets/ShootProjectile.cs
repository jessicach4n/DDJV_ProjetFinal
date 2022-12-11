using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShootProjectile : MonoBehaviour
{
    public float m_Speed = 10f;
    private Rigidbody2D m_Rigidbody;
    public float levier;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //J'ai séparé les deux. Clairement, AddForceAtPostion a un bug. -MAL
        m_Rigidbody.AddTorque(levier,ForceMode2D.Impulse);
        m_Rigidbody.AddForce(transform.right * m_Speed,ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}