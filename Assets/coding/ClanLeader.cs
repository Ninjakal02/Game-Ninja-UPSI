using UnityEngine;
using UnityEngine.UI;

public class ClanLeader : MonoBehaviour
{
    [Header("UI & Audio")]
    public GameObject dialogueBox;    // Panel dialog
    public Text dialogueText;         // UI Text biasa
    public AudioSource audioSource;   // audio source untuk suara

    [Header("Dialogues")]
    public string[] dialogues;
    public AudioClip[] voiceClips;

    private Player player;
    private int currentDialogue = 0;
    private bool isTalking = false;
    private bool hasTalked = false;

    void Start()
    {
        if (dialogueBox != null)
            dialogueBox.SetActive(false);
    }

    void Update()
    {
        if (isTalking && Input.GetKeyDown(KeyCode.Z))
        {
            NextDialogue();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTalked && other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
            if (player != null)
                player.canMove = false;

            StartDialogue();
            hasTalked = true;
        }
    }

    void StartDialogue()
    {
        if (dialogueBox != null)
            dialogueBox.SetActive(true);

        currentDialogue = 0;
        ShowDialogue(currentDialogue);
        isTalking = true;
    }

    void ShowDialogue(int index)
    {
        if (dialogueText != null && index < dialogues.Length)
            dialogueText.text = dialogues[index];

        if (audioSource != null && voiceClips != null && index < voiceClips.Length && voiceClips[index] != null)
        {
            audioSource.clip = voiceClips[index];
            audioSource.Play();
        }
    }

    void NextDialogue()
    {
        currentDialogue++;

        if (currentDialogue < dialogues.Length)
        {
            ShowDialogue(currentDialogue);
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        if (dialogueBox != null)
            dialogueBox.SetActive(false);

        if (player != null)
            player.canMove = true;

        isTalking = false;
        Debug.Log("Player boleh bergerak balik");
    }
}