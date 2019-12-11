using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public UIManager uIManager;
    
    public float winPercentage;

    private float progress;

    [HideInInspector]
    public int targetCount;
    [HideInInspector]
    public int demolishedTargets;
    [HideInInspector]
    public int shots;



    void Start()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;

        uIManager.SetProgressTarget(winPercentage);
    }

    void Update()
    {
    }

    public void TargetDemolished()
    {
        demolishedTargets++;
        progress = (float)demolishedTargets / targetCount;
        uIManager.SetProgress(progress);
        if(progress*100 > winPercentage)
        {
            Debug.Log("You Win!");
        }
    }

    public void ProjectileShot()
    {
        shots++;
        uIManager.SetShots(shots);
    }
}
