using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private AudioClip npcVoice;
    [SerializeField] private AudioClip playerVoice;
    [SerializeField] private int charsToPlaySound;
    // Se debe marcar si el jugador hablará primero
    [SerializeField]private bool isPlayerTalking;

    [SerializeField] private float typingTime;
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;

    private AudioSource audioSource;
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;



    private void Start()
    {
      audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if(dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];

                // Si hay n-dialogos y son pares, este se activará
                // para que se reinicie el cambio de audio
                // Este if se colocó debido a que los dialogos se intercambiaban
                // Entre el player y el NPC, en otros casos se puede quitar
                if (lineIndex == dialogueLines.Length - 1 &&
                 dialogueLines.Length%2==0)
                {
                  isPlayerTalking =!isPlayerTalking;
                }
            }

        }
    }

    // Cuando se inicia el dialogo se activa el panel donde se escribe el dialogo...
    // El tiempo se detiene para evitar movimiento del player, el lineindex se..
    // hace 0 para empezar por el primer dialogo.
    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    // Esta función hace que se escriba en la pantalla el siguiente dialogo.
    private void NextDialogueLine()
    {
        lineIndex++;
        if(lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    // Esta funcion se puede cambiar a conveniencia dependiendo de lo que se...
    // Necesite en el momento o como sean pensados los dialogos.
    private void SelectAudioClip()
    {
      if (lineIndex != 0)
      {
        isPlayerTalking = !isPlayerTalking;
      }

      // Operador alternario
      audioSource.clip = isPlayerTalking ? playerVoice : npcVoice;
    }

    private IEnumerator ShowLine()
    {
        SelectAudioClip();
        dialogueText.text = string.Empty;
        int charIndex = 0;

        foreach(char ch in dialogueLines[lineIndex])
        {
            dialogueText.text+= ch;

            if (charIndex % charsToPlaySound == 0)
            {
              audioSource.Play();
            }


            charIndex++;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    // Cuando se entre en el colliderbox que funciona como trigger...
    // Se activara un sprite encima del npc para indicar que se puede conversar.
    // El sprite debe ser hijo de la clase del npc
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
        }

    }
    // Cuando se entre en conversación el sprite encima del NPC desaparecerá
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
        }
    }
}
