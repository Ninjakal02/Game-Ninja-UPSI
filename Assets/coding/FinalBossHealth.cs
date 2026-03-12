using UnityEngine;
using UnityEngine.UI;

public class FinalBossHealth : MonoBehaviour
{
    public int maxHealth = 200;
    private int currentHealth;

    public Image bossHPBar;
    public GameObject bossUI;

    void Start()
    {
        currentHealth = maxHealth;

        if (bossUI != null)
            bossUI.SetActive(false);

        UpdateHPBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (bossUI != null && !bossUI.activeSelf)
            bossUI.SetActive(true);

        UpdateHPBar();
        Debug.Log("Boss kena damage: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHPBar();

        if (bossUI != null)
            bossUI.SetActive(false);

        gameObject.SetActive(true);
    }

    void UpdateHPBar()
    {
        if (bossHPBar != null)
            bossHPBar.fillAmount = (float)currentHealth / maxHealth;
    }

    void Die()
    {
        Debug.Log("Boss mati!");
        gameObject.SetActive(false);
    }
}