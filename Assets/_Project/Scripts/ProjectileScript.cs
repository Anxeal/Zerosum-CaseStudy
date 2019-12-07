using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Rigidbody rb;

    private bool collided = false;

    private Vector3 windForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    void FixedUpdate()
    {
        if (!collided) { 
            rb.AddForce(windForce);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // disable pre-collision curve
        collided = true;
    }

    public void Launch(Vector3 force, Vector3 curve)
    {
        rb.isKinematic = false;
        rb.AddForce(force);
        windForce = curve;
    }
}
