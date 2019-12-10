using System.Collections.Generic;
using UnityEngine;

public interface ITrajectoryDrawer
{
    void SetTrajectory(List<Vector3> trajectory);
    void HideTrajectory();
    void ShowTrajectory();
}
