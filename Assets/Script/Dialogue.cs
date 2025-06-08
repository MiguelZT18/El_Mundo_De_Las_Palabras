using UnityEngine;
using System.Collections;
using TMPro;

/// <summary>
/// Controla el sistema de diálogo interactivo.
/// Muestra líneas de texto cuando el jugador se encuentra en un área determinada y presiona la tecla espacio.
/// </summary>
public class Dialogue : MonoBehaviour
{
    /// <summary>
    /// Indica si el jugador está dentro del área de interacción.
    /// </summary>
    private bool isPlayerInRange;

    /// <summary>
    /// Líneas de diálogo que se mostrarán. Se pueden definir en el Inspector.
    /// </summary>
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    /// <summary>
    /// Panel visual que contiene el texto del diálogo.
    /// </summary>
    [SerializeField] private GameObject dialoguePanel;

    /// <summary>
    /// Objeto de texto TMP donde se muestra el diálogo.
    /// </summary>
    [SerializeField] private TMP_Text dialogueText;

    /// <summary>
    /// Controla si el diálogo ha comenzado.
    /// </summary>
    private bool didDialogueStart;

    /// <summary>
    /// Índice actual de la línea de diálogo que se está mostrando.
    /// </summary>
    private int lineIndex;

    /// <summary>
    /// Tiempo de espera entre cada letra al escribir.
    /// </summary>
    private float typingTime = 0.05f;

    /// <summary>
    /// Escucha la entrada del jugador para iniciar o continuar el diálogo.
    /// </summary>
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    /// <summary>
    /// Detecta si el jugador ha entrado al área de diálogo.
    /// </summary>
    /// <param name="collision">Colisión detectada.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    /// <summary>
    /// Detecta si el jugador ha salido del área de diálogo.
    /// </summary>
    /// <param name="collision">Colisión detectada.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    /// <summary>
    /// Inicia el diálogo, activa el panel y pausa el tiempo del juego.
    /// </summary>
    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        Time.timeScale = 0f;
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    /// <summary>
    /// Muestra gradualmente la línea actual del diálogo.
    /// </summary>
    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    /// <summary>
    /// Avanza a la siguiente línea del diálogo o finaliza el diálogo si no hay más líneas.
    /// </summary>
    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
