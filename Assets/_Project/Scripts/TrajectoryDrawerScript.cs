using System.Collections.Generic;
using UnityEngine;

public class TrajectoryDrawerScript : MonoBehaviour, ITrajectoryDrawer
{
    public GameObject trajectoryPrefab;
    public GameObject impactPrefab;

    private GameObject container;
    private List<GameObject> points = new List<GameObject>();
    private GameObject impact;
    private int steps;

    void Start()
    {
        container = new GameObject("Trajectory Container");
        steps = GetComponent<ProjectileShooterScript>().predictionSteps;
        for (int i = 0; i < steps; i++)
        {
            points.Add(Instantiate(trajectoryPrefab, container.transform));
        }
        impact = Instantiate(impactPrefab, container.transform);
    }

    public void SetTrajectory(List<Vector3> trajectory)
    {
        int i;
        for (i = 0; i < trajectory.Count; i++)
        {
            points[i].SetActive(true);
            points[i].transform.position = trajectory[i];
        }
        for (; i < steps; i++)
        {
            points[i].SetActive(false);
        }
        impact.transform.position = trajectory[trajectory.Count - 1];
    }

    public void ShowTrajectory()
    {
        container.SetActive(true);
    }

    public void HideTrajectory()
    {
        container.SetActive(false);
    }
}
