using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public float maxHealth = 100f;

    public Image hpBar;

    void Start()
    {
        UpdateHPBar();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log("Player kena damage! Health: " + health);

        UpdateHPBar();

        if (health <= 0)
        {
            Debug.Log("Player mati");
        }
    }

    void UpdateHPBar()
    {
        hpBar.fillAmount = health / maxHealth;
    }
}