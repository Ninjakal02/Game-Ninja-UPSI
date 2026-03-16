using TMPro;
using UnityEngine;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    [TextArea(3, 5)]
    public string[] lines;

    [HideInInspector]
    public int currentLine = 0;

    public int maxLineToShow = 1;

    // ==== Audio Typing ====
    public AudioSource typingSound;
    public float typeSpeed = 0.03f;

    private Coroutine typingCoroutine;

    void Start()
    {
        dialogueBox.SetActive(true);
        currentLine = 0;

        // FREEZE SEMUA
        Time.timeScale = 0f;

        StartTyping(lines[currentLine]);
    }

    void Update()
    {
        if (dialogueBox.activeSelf && Input.GetKeyDown(KeyCode.Z))
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = lines[currentLine];
                typingCoroutine = null;
            }
            else
            {
                NextLine();
            }
        }
    }

    public void StartDialogue(int startLine)
    {
        dialogueBox.SetActive(true);
        currentLine = startLine;

        // Freeze game
        Time.timeScale = 0f;

        if (currentLine < lines.Length)
            StartTyping(lines[currentLine]);
    }

    public void NextLine()
    {
        currentLine++;

        if (currentLine <= maxLineToShow && currentLine < lines.Length)
        {
            StartTyping(lines[currentLine]);
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);

        // GAME BERJALAN BALIK
        Time.timeScale = 1f;
    }

    void StartTyping(string line)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeSentence(line));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;

            if (typingSound != null)
                typingSound.PlayOneShot(typingSound.clip);

            yield return new WaitForSecondsRealtime(typeSpeed);
        }

        typingCoroutine = null;
    }
}