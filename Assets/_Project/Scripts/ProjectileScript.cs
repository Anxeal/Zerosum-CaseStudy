using DG.Tweening;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Rigidbody rb;

    private bool collided = false;

    public bool launched;

    private Vector3 windForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        transform.DOScale(0, .3f).From().SetEase(Ease.OutBounce);
    }

    void FixedUpdate()
    {
        if (!collided)
        {
            rb.AddForce(windForce);
        }

        // destroy projectile if it falls off
        if (transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // disable pre-collision curve
        collided = true;
    }

    public void Launch(Vector3 force, Vector3 curve)
    {
        launched = true;
        rb.isKinematic = false;
        rb.AddForce(force);
        windForce = curve;
    }
}
