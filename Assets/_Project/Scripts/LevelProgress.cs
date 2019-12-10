using UnityEngine;
using UnityEngine.UI;

public class LevelProgress : MonoBehaviour
{
    public Slider progressSlider, targetSlider;
    public float speed;

    private float targetProgress;

    void Update()
    {
        progressSlider.value = Mathf.Lerp(progressSlider.value, targetProgress, Time.deltaTime * speed);
    }

    public void SetProgress(float value)
    {
        targetProgress = value;
    }

    public void ResetProgress()
    {
        progressSlider.value = 0;
        targetProgress = 0;
    }

    public void SetTarget(float value)
    {
        targetSlider.value = value / 100f;
        ResetProgress();
    }
}
