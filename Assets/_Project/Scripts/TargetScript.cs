using UnityEngine;

public class TargetScript : MonoBehaviour
{
    // the block is considered a target if it drops below this height
    public float successHeight;

    private GameManager gameManager;
    private bool demolished;

    void Start()
    {
        gameManager = GameManager.Instance;

        // check if demolished from start
        if (transform.localPosition.y < successHeight)
        {
            demolished = true;
        }
        else
        {
            gameManager.targetCount++;
        }
    }

    void Update()
    {
        if (transform.position.y < 0)
        {
            Destroy(gameObject);
        }
        else if (!demolished && transform.localPosition.y < successHeight)
        {
            demolished = true;
            gameManager.TargetDemolished();
        }
    }
}
