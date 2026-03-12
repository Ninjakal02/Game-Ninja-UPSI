using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;

    public Image hpBar;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHPBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHPBar();
        Debug.Log(name + " kena damage: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHPBar();
        gameObject.SetActive(true);
    }

    void UpdateHPBar()
    {
        if (hpBar != null)
            hpBar.fillAmount = (float)currentHealth / maxHealth;
    }

    void Die()
    {
        Debug.Log(name + " mati!");
        gameObject.SetActive(false);
    }
}