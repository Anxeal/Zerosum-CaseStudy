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

    public Slider progressSlider;

    private float progress;


    void Start()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    void Update()
    {
        progressSlider.value = Mathf.Lerp(progressSlider.value, progress, Time.deltaTime*5);
    }

    public void TargetDemolished()
    {
        demolishedTargets++;
        progress = (float)demolishedTargets / targetCount;
    }
}
