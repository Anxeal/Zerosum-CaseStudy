using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooterScript : MonoBehaviour
{

    public GameObject projectilePrefab;
    public float launchForce;

    private GameObject latestProjectile;
    private Vector2 screenPos, mousePos, posDiff;
    private bool dragging, canLaunch;
    
    void Start()
    {
        SpawnProjectile();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            screenPos = Camera.main.WorldToScreenPoint(transform.position);
            dragging = true;
        }

        if (dragging)
        {
            mousePos.x = Input.mousePosition.x;
            mousePos.y = Input.mousePosition.y;
            Vector2 posDiff = mousePos - screenPos;

            DrawTrajectory();

            if (Input.GetMouseButtonUp(0))
            {
                dragging = false;
                if (canLaunch) {
                LaunchProjectile();
            }
        }
    }
    }

    private void SpawnProjectile()
    {
        latestProjectile = Instantiate(projectilePrefab, transform);
        canLaunch = true;
    }

    private void LaunchProjectile()
    {
        ProjectileScript ps = latestProjectile.GetComponent<ProjectileScript>();
        ps.Launch((transform.forward+transform.up)*launchForce);
        Invoke("SpawnProjectile", 1f);
        canLaunch = false;
    }

    private void DrawTrajectory()
    {
        // TODO: draw predictive projectile trajectory
    }
}
