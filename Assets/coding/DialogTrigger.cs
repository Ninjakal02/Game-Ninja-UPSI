using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueSystem dialogue;
    public int startLine = 2; // line ke 3 (array start dari 0)

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogue.StartDialogue(startLine);
        }
    }
}