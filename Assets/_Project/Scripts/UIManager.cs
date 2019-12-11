using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public LevelProgress levelProgress;
    public TextMeshProUGUI shotsText;
    public TextMeshProUGUI levelText;

    public void SetProgress(float value)
    {
        levelProgress.SetProgress(value);
    }

    public void ResetProgress()
    {
        levelProgress.ResetProgress();
    }

    public void SetProgressTarget(float value)
    {
        levelProgress.SetTarget(value);
    }

    public void SetShots(int value)
    {
        shotsText.SetText(value.ToString());
    }

    public void SetLevel(int value)
    {
        levelText.SetText(value.ToString());
    }
}
