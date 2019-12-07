using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooterScript : MonoBehaviour
{

    public GameObject projectilePrefab;

    public Rect inputLimit;

    public float launchForce;
    public float curveForce;
    public float horizontalAngleLimit;
    public float minVerticalAngleLimit;
    public float maxVerticalAngleLimit;

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
            posDiff = mousePos - screenPos;

            posDiff.x = Mathf.Clamp(posDiff.x, inputLimit.xMin, inputLimit.xMax);
            posDiff.y = Mathf.Clamp(posDiff.y, inputLimit.yMin, inputLimit.yMax);

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
        float horizontalValue = Mathf.InverseLerp(inputLimit.xMin, inputLimit.xMax, posDiff.x);
        float verticalValue = Mathf.InverseLerp(inputLimit.yMin, inputLimit.yMax, posDiff.y);

        float horizontalAngle = -Mathf.Lerp(-horizontalAngleLimit, horizontalAngleLimit, horizontalValue);
        float verticalAngle = -Mathf.Lerp(maxVerticalAngleLimit, minVerticalAngleLimit, verticalValue);
        
        Vector3 launchDir = Quaternion.Euler(verticalAngle, horizontalAngle, 0) * transform.forward;

        ps.Launch(launchDir * launchForce, GetCurve(launchDir));
        Invoke("SpawnProjectile", 1f);
        canLaunch = false;
    }

    private Vector3 GetCurve(Vector3 direction)
    {
        return -transform.right * Mathf.Sign(direction.x)*direction.x*direction.x * curveForce;
    }

    private void DrawTrajectory()
    {
        // TODO: draw predictive projectile trajectory
    }
}
