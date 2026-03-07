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

    void Start()
    {
        health = maxHealth;
        UpdateHPBar();

        playerMovement = GetComponent<Player>();
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

        // Stop movement script sahaja
        if (playerMovement != null)
            playerMovement.enabled = false;
    }
}