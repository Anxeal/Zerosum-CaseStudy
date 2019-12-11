using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public UIManager uIManager;

    public float winPercentage;
    public int level = 1;

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
        if (progress * 100 > winPercentage)
        {
            Debug.Log("You Win!");
            NextLevel();
        }
    }

    public void ProjectileShot()
    {
        shots++;
        uIManager.SetShots(shots);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level" + level, LoadSceneMode.Additive);
    }

    public void NextLevel()
    {
        level++;
        LoadLevel();
    }
}
