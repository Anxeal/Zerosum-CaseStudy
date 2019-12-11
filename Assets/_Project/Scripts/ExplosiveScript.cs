using UnityEngine;
using DG.Tweening;

public class ExplosiveScript : MonoBehaviour
{
    [Header("Explosion")]
    public float explosionRadius;
    public float explosionForce;
    public GameObject explosionEffect;

    [Header("Camera Shake")]
    public float shakeDuration;
    public float shakeStrength;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Camera.main.DOShakeRotation(shakeDuration, shakeStrength);


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
