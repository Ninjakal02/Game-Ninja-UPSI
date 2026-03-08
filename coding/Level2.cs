using UnityEngine;

public class Level2Controller : MonoBehaviour
{
    public int sabotageTargets = 3;
    private int destroyedTargets = 0;

    public GameObject escapeZone;

    public void TargetDestroyed()
    {
        destroyedTargets++;

        if (destroyedTargets >= sabotageTargets)
        {
            escapeZone.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (destroyedTargets >= sabotageTargets && collision.CompareTag("Player"))
        {
            LevelManager.Instance.CompleteLevel();
        }
    }
}