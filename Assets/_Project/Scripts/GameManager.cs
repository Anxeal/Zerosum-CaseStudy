using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int targetCount;
    public int demolishedTargets;

    void Start()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    public void TargetDemolished()
    {
        demolishedTargets++;
        Debug.Log(100.0f*demolishedTargets / targetCount);
    }
}
