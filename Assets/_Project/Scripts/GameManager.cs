using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [HideInInspector]
    public int targetCount;
    [HideInInspector]
    public int demolishedTargets;

    public LevelProgress levelProgress;

    private float progress;

    public float winPercentage;


    void Start()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;

        levelProgress.SetTarget(winPercentage);
    }

    void Update()
    {
    }

    public void TargetDemolished()
    {
        demolishedTargets++;
        progress = (float)demolishedTargets / targetCount;
        levelProgress.SetProgress(progress);
        if(progress*100 > winPercentage)
        {
            Debug.Log("You Win!");
        }
    }
}
