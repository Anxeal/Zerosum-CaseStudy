using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooterScript : MonoBehaviour
{
    public GameObject projectilePrefab;

    [Header("Force")]
    public float launchForce;
    public float curveForce;
    
    [Header("Limit")]
    public Rect inputLimit;
    public float horizontalAngleLimit;
    public float minVerticalAngleLimit;
    public float maxVerticalAngleLimit;

    public float projectileCooldown;

    [Header("Trajectory Prediction")]
    public int predictionSteps;
    public float predictionTimestep;
    private ITrajectoryDrawer[] trajectoryDrawers;

    private GameObject latestProjectile;
    private Vector2 screenPos, mousePos, posDiff;
    private bool dragging, canLaunch;

    private GameManager gameManager;

    void Start()
    {
        SpawnProjectile();
        trajectoryDrawers = GetComponents<ITrajectoryDrawer>();

        gameManager = GameManager.Instance;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            screenPos = Camera.main.WorldToScreenPoint(transform.position);
            foreach (var td in trajectoryDrawers) td.ShowTrajectory();
            dragging = true;
        }

        if (dragging)
        {
            mousePos.x = Input.mousePosition.x;
            mousePos.y = Input.mousePosition.y;
            posDiff = mousePos - screenPos;

            posDiff.x = Mathf.Clamp(posDiff.x, inputLimit.xMin, inputLimit.xMax);
            posDiff.y = Mathf.Clamp(posDiff.y, inputLimit.yMin, inputLimit.yMax);


            foreach (var td in trajectoryDrawers) td.SetTrajectory(PredictTrajectory());

            if (Input.GetMouseButtonUp(0))
            {
                foreach (var td in trajectoryDrawers) td.HideTrajectory();
                dragging = false;
                if (canLaunch)
                {
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

        Vector3 launchVector = GetLaunchVector();

        ps.Launch(launchVector, GetCurve(launchVector));
        Invoke("SpawnProjectile", projectileCooldown);
        canLaunch = false;

        gameManager.ProjectileShot();
    }

    private Vector3 GetLaunchVector()
    {
        float horizontalValue = Mathf.InverseLerp(inputLimit.xMin, inputLimit.xMax, posDiff.x);
        float verticalValue = Mathf.InverseLerp(inputLimit.yMin, inputLimit.yMax, posDiff.y);

        float horizontalAngle = -Mathf.Lerp(-horizontalAngleLimit, horizontalAngleLimit, horizontalValue);
        float verticalAngle = -Mathf.Lerp(maxVerticalAngleLimit, minVerticalAngleLimit, verticalValue);

        Vector3 launchDir = Quaternion.Euler(verticalAngle, horizontalAngle, 0) * transform.forward;

        return launchDir * launchForce;
    }

    private Vector3 GetCurve(Vector3 direction)
    {
        direction /= launchForce;
        // x>0 => x^2
        // x<0 => -x^2
        // curve becomes more apparent towards the sides
        return -transform.right * Mathf.Sign(direction.x) * direction.x * direction.x * curveForce;
    }

    private List<Vector3> PredictTrajectory()
    {
        Vector3 initialPosition = transform.position;
        Vector3 launchVector = GetLaunchVector();
        Vector3 curveVector = GetCurve(launchVector);
        Vector3 initialSpeed = launchVector * Time.fixedDeltaTime;
        Vector3 acceleration = curveVector + Physics.gravity;

        List<Vector3> pos = new List<Vector3>();
        pos.Add(initialPosition);

        for (int i = 1; i < predictionSteps; i++)
        {
            float t = i * predictionTimestep;
            pos.Add(initialPosition + initialSpeed * t + acceleration * t * t / 2);
            Vector3 dir = pos[i] - pos[i - 1];
            if (Physics.Raycast(pos[i - 1], dir, out RaycastHit hit, dir.magnitude))
            {
                pos[i] = hit.point;
                break;
            }
        }

        return pos;
    }

}
