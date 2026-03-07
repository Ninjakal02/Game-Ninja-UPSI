using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public float maxHealth = 100f;

    public Image hpBar;
    public Animator animator;

    bool isDead = false;

    void Start()
    {
        UpdateHPBar();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;

        UpdateHPBar();

        Debug.Log("Player kena damage! Health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void UpdateHPBar()
    {
        hpBar.fillAmount = (float)health / maxHealth;
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        Debug.Log("Player mati");

        animator.SetBool("isDead", true);
    }
}