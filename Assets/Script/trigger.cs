using UnityEngine;

/// <summary>
/// Activa o desactiva un panel UI cuando el jugador entra o sale del trigger.
/// </summary>
public class EnemyTrigger : MonoBehaviour
{
    [Tooltip("Panel UI que se mostrará u ocultará")]
    public GameObject panelUI; // Asignar en el Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            panelUI.SetActive(true); // Mostrar el panel al entrar el jugador
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            panelUI.SetActive(false); // Ocultar el panel al salir el jugador
        }
    }
}
