using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

/// <summary>
/// Controla la pantalla de felicitación que se muestra al completar la misión,
/// mostrando el puntaje final y luego regresa al menú principal.
/// </summary>
public class PantallaFelicitacion : MonoBehaviour
{
    /// <summary>
    /// Texto UI donde se muestra el mensaje de felicitación y el puntaje.
    /// </summary>
    public TextMeshProUGUI textoPuntaje;

    /// <summary>
    /// Nombre de la escena a la que se regresa después de mostrar la pantalla.
    /// </summary>
    public string escenaMenu = "MenuPrincipal";

    /// <summary>
    /// Método llamado al iniciar el script, muestra el mensaje y comienza la espera para volver al menú.
    /// </summary>
    void Start()
    {
        // Obtener el puntaje total desde el GameManager (0 si no existe)
        int puntaje = GameManager.Instance != null ? GameManager.Instance.PuntosTotales : 0;

        // Actualizar el texto con la felicitación, créditos y puntaje
        textoPuntaje.text = $"¡Felicidades!\nHas completado tu misión de derrotar a Anton.\nCreditos: \nMiguel Ángel Zamora Téllez\nWen Urbina\nPuntaje total: {puntaje}";

        // Iniciar la corrutina que espera y luego cambia a la escena del menú
        StartCoroutine(VolverAlMenu());
    }

    /// <summary>
    /// Corrutina que espera 5 segundos y luego carga la escena del menú principal.
    /// </summary>
    IEnumerator VolverAlMenu()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(escenaMenu);
    }
}
