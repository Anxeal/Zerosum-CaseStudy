using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Level
    {
        get => level;
        set
        {
            level = value;
            uIManager.SetLevel(level);
        }
    }

    public int Shots
    {
        get => shots;
        set
        {
            shots = value;
            uIManager.SetShots(shots);
        }
    }

    public UIManager uIManager;

    public float winPercentage;
    private int level = 1;
    public int totalLevels;

    private float progress;

    [HideInInspector]
    public int targetCount;
    [HideInInspector]
    public int demolishedTargets;

    private int shots;

    void Start()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;

        uIManager.SetProgressTarget(winPercentage);

        LoadLevel();
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
        Shots++;
    }

    public void LoadLevel()
    {
        // reset everything first

        progress = 0;
        uIManager.ResetProgress();
        targetCount = 0;
        demolishedTargets = 0;

        SceneManager.LoadScene("Level" + Level, LoadSceneMode.Additive);
    }
    public void NextLevel()
    {
        StartCoroutine(NextLevelCoroutine());
    }

    public IEnumerator NextLevelCoroutine()
    {
        var loaded = SceneManager.UnloadSceneAsync("Level" + Level);
        yield return loaded.isDone;
        if (Level == totalLevels)
        {
            uIManager.ShowRestart();
            yield break;
        }
        Level++;
        LoadLevel();
    }

    public void RestartGame()
    {
        Level = 1;
        Shots = 0;
        LoadLevel();
        uIManager.HideRestart();
    }
    
}
