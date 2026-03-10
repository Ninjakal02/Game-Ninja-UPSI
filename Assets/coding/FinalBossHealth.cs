using UnityEngine;
using UnityEngine.UI;

public class FinalBossHealth : MonoBehaviour
{
    [Header("Boss Health")]
    public int maxHealth = 200;
    private int currentHealth;

    [Header("Boss UI")]
    public Image bossHPBar;     // HP bar merah
    public GameObject bossUI;   // Canvas boss HP

    void Start()
    {
        currentHealth = maxHealth;

        if (bossUI != null)
            bossUI.SetActive(false); // HP bar sembunyi dulu

        UpdateHPBar();
    }

    void UpdateHPBar()
    {
        if (bossHPBar != null)
        {
            bossHPBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (bossUI != null && !bossUI.activeSelf)
            bossUI.SetActive(true); // bila kena attack baru keluar HP bar

        UpdateHPBar();

        Debug.Log("Boss kena damage: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Boss mati!");

        Destroy(gameObject);
    }
}