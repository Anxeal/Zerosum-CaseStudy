using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public LevelProgress levelProgress;
    public TextMeshProUGUI shotsText;

    public void SetProgress(float value)
    {
        levelProgress.SetProgress(value);
    }

    public void SetProgressTarget(float value)
    {
        levelProgress.SetTarget(value);
    }

    public void SetShots(int value)
    {
        shotsText.SetText(value.ToString());
    }
}
