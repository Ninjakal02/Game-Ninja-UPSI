using UnityEngine;

public class ClanLeader : MonoBehaviour
{
    public GameObject dialogueBox;

    void Start()
    {
        if (dialogueBox != null)
            dialogueBox.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (dialogueBox != null)
                dialogueBox.SetActive(true);

            Debug.Log("Terima kasih kerana selamatkan saya!");
        }
    }
}