using UnityEngine;

public class Level3Controller : MonoBehaviour
{
    public GameObject tokugawa;
    public GameObject safeZone;

    private bool rescued = false;
    private bool bossDead = false;

    void Start()
    {
        safeZone.SetActive(false);
    }

    public void RescueTokugawa()
    {
        rescued = true;
        CheckProgress();
    }

    public void BossDefeated()
    {
        bossDead = true;
        CheckProgress();
    }

    void CheckProgress()
    {
        if (rescued && bossDead)
        {
            safeZone.SetActive(true);
            Debug.Log("Escort to Safe Zone!");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && rescued && bossDead)
        {
            LevelManager.Instance.CompleteLevel();
        }
    }
}