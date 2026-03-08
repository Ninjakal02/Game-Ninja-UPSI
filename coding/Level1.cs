using UnityEngine;

public class Level1Controller : MonoBehaviour
{
    public int enemiesToKill = 10;
    private int killed = 0;

    public void EnemyKilled()
    {
        killed++;

        if (killed >= enemiesToKill)
        {
            LevelManager.Instance.CompleteLevel();
        }
    }
}