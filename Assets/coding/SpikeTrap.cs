using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private Animator anim;
    public int damage = 1;
    private bool activated = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            activated = true;

            anim.SetTrigger("Close");

            PlayerHealth player = other.GetComponent<PlayerHealth>();

            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }
}