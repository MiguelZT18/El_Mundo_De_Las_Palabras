using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controla la transición entre escenas cuando el jugador entra en un área específica.
/// </summary>
public class SceneTransition : MonoBehaviour
{
    /// <summary>
    /// Nombre de la escena a la que se debe cambiar.
    /// </summary>
    public string nombreEscenaDestino;

    /// <summary>
    /// Posición en la que el jugador aparecerá en la nueva escena.
    /// </summary>
    public Vector2 posicionSpawn;

    /// <summary>
    /// Detecta la entrada del jugador en el área de transición y cambia de escena,
    /// guardando la posición de aparición del jugador.
    /// </summary>
    /// <param name="other">El collider del objeto que entra en el trigger.</param>
    /// <remarks>
    /// Este método utiliza PlayerPrefs para guardar la posición de aparición
    /// y luego llama a SceneManager.LoadScene para cargar la nueva escena.
    /// </remarks>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Guarda la posición donde debe aparecer en la nueva escena
            PlayerPrefs.SetFloat("SpawnX", posicionSpawn.x);
            PlayerPrefs.SetFloat("SpawnY", posicionSpawn.y);

            // Cambia de escena
            SceneManager.LoadScene(nombreEscenaDestino);
        }
    }
}

