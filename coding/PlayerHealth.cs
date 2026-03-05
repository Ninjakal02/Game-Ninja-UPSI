using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 30;
    public int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;

        Debug.Log("Player HP: " + currentHP);

        if (currentHP <= 0) 
        {
            currentHP = 0;   
            Die();
        }
    }

    public void HealOnKill()
    {
        currentHP += 3;

        if (currentHP > maxHP)
            currentHP = maxHP;

        Debug.Log("HP: " + currentHP);
    }

    void Die()
    {
        Debug.Log("Player Dead");
        LevelManager.Instance.GameOver();
    }
}