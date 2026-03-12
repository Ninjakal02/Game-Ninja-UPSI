using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;

    public Image hpBar;
    public Animator animator;

    private bool isDead = false;
    private Player playerMovement;
    private PlayerRespawn respawn;

    void Start()
    {
        health = maxHealth;
        UpdateHPBar();

        playerMovement = GetComponent<Player>();
        respawn = GetComponent<PlayerRespawn>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;

        if (health < 0)
            health = 0;

        UpdateHPBar();

        Debug.Log("Player kena damage! Health: " + health);

        if (health == 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        health += amount;

        if (health > maxHealth)
            health = maxHealth;

        UpdateHPBar();

        Debug.Log("Player heal: " + health);
    }

    void UpdateHPBar()
    {
        if (hpBar != null)
            hpBar.fillAmount = (float)health / maxHealth;
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        Debug.Log("Player mati");

        if (animator != null)
            animator.SetBool("isDead", true);

        // Stop movement
        if (playerMovement != null)
            playerMovement.enabled = false;

        // Respawn selepas 2 saat
        Invoke("RespawnPlayer", 2f);
    }

    void RespawnPlayer()
    {
        if (respawn != null)
        {
            respawn.Respawn();
        }

        // Reset health
        health = maxHealth;
        UpdateHPBar();

        // Reset animation
        if (animator != null)
            animator.SetBool("isDead", false);

        // Enable movement balik
        if (playerMovement != null)
            playerMovement.enabled = true;

        isDead = false;
    }
}