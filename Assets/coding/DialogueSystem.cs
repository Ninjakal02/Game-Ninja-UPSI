using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    [TextArea(3, 5)]
    public string[] lines;

    [HideInInspector]
    public int currentLine = 0;

    // Tambah ni ? max line yang nak dipaparkan
    public int maxLineToShow = 1; // line 0 & 1

    void Start()
    {
        dialogueBox.SetActive(true); // paparkan panel
        currentLine = 0;
        dialogueText.text = lines[currentLine]; // paparkan line 0
    }

    void Update()
    {
        if (dialogueBox.activeSelf && Input.GetKeyDown(KeyCode.Z))
        {
            NextLine();
        }
    }

    public void StartDialogue(int startLine)
    {
        dialogueBox.SetActive(true);
        currentLine = startLine;

        if (currentLine < lines.Length)
            dialogueText.text = lines[currentLine];
    }

    public void NextLine()
    {
        currentLine++;

        // Stop dialog selepas line 1 sahaja
        if (currentLine <= maxLineToShow && currentLine < lines.Length)
        {
            dialogueText.text = lines[currentLine];
        }
        else
        {
            dialogueBox.SetActive(false);
        }
    }
}