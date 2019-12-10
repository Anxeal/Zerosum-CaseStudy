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

    public void SetShots(int value)
    {
        shotsText.SetText("Shots: "+value);
    }
}
