using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LayerMask masqueRaycast;
    public float maxDistance;
    public mouvementRadish radish;
    private VolumetricLines.VolumetricLineBehavior laser;

    void Start()
    {
        laser = GetComponent<VolumetricLines.VolumetricLineBehavior>();
        laser.StartPos = new Vector3(0.0f, 0.0f, 0.0f);
        laser.EndPos = new Vector3(0.0f, 500.0f, 0.0f);
        maxDistance = 1000.0f;
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, maxDistance, masqueRaycast);

        if (radish.nbPiesCollected == radish.maxPies)
        {
            Destroy(gameObject);

        }
        else if (hit.collider != null)
        {
            laser.EndPos = new Vector3(0.0f, hit.distance, 0.0f);

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                hit.collider.gameObject.GetComponent<mouvementRadish>().OnHitLazer();
            }
        }
        else
        {
            laser.EndPos = new Vector3(0.0f, 500.0f, 0.0f);
        }


    }
}
