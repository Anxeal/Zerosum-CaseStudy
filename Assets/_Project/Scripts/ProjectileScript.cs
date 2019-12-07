using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    void OnCollisionEnter(Collision collision)
    {
        // TODO: add collision logic
    }

    public void Launch(Vector3 force)
    {
        rb.isKinematic = false;
        rb.AddForce(force);
    }
}
