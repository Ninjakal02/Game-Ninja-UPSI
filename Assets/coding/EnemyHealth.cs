using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    int currentHealth;

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

        Debug.Log("Enemy kena damage: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHPBar()
    {
        if (hpBar != null)
            hpBar.fillAmount = (float)currentHealth / maxHealth;
    }

    void Die()
    {
        Debug.Log("Enemy mati");

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            PlayerHealth ph = player.GetComponent<PlayerHealth>();

            if (ph != null)
            {
                ph.Heal(3);
            }
        }

        Destroy(gameObject);
    }
}