using UnityEngine;
using UnityEngine.UI;

public class FinalBossHealth : MonoBehaviour
{
    [Header("Boss Health")]
    public int maxHealth = 200;
    private int currentHealth;

    [Header("Boss UI")]
    public Image bossHPBar;
    public GameObject bossUI;

    [Header("Environment")]
    public GameObject wall; // dinding yang menghalang player
    public CageController cage; // controller untuk buka sangkar ketua bandar

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

        // buka laluan
        if (wall != null)
        {
            wall.SetActive(false);
        }

        // buka sangkar ketua bandar
        if (cage != null)
        {
            cage.OpenCage();
        }

        // matikan boss
        gameObject.SetActive(false);
    }
}