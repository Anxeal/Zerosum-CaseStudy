using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoTrajectoryScript : MonoBehaviour, ITrajectoryDrawer
{
    private bool visible;
    private List<Vector3> trajectory;

    void Start()
    {
        
    }

    void OnDrawGizmos()
    {
        if (visible)
        {
            for (int i = 0; i < trajectory.Count - 1; i++)
            {
                Gizmos.DrawLine(trajectory[i], trajectory[i + 1]);
            }
        }
    }

    public void SetTrajectory(List<Vector3> trajectory)
    {
        this.trajectory = trajectory;
    }

    public void ShowTrajectory()
    {
        visible = true;
    }

    public void HideTrajectory()
    {
        visible = false;
    }
}
