using UnityEngine;

public class ExplosiveScript : MonoBehaviour
{
    public float explosionRadius;
    public float explosionForce;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider col in nearbyColliders)
            {
                Rigidbody rb = col.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }

            Destroy(gameObject);
        }

    }
}
