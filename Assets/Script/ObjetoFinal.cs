using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controla el comportamiento del objeto final que al ser recogido por el jugador,
/// guarda el progreso y cambia a una escena de felicitación.
/// </summary>
public class ObjetoFinal : MonoBehaviour
{
    /// <summary>
    /// Nombre de la escena a cargar al recoger el objeto (por defecto "EscenaFelicitacion").
    /// </summary>
    public string nombreEscena = "EscenaFelicitacion";

    /// <summary>
    /// Detecta cuando el jugador entra en contacto con el objeto.
    /// </summary>
    /// <param name="other">Collider del objeto que entró en contacto.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // Solo se activa si el objeto que colisiona tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            // Guardar progreso indicando que el diccionario fue recogido
            PlayerPrefs.SetInt("DiccionarioRecogido", 1);

            // Destruir el objeto para que no pueda ser recogido otra vez
            Destroy(gameObject);

            // Cambiar a la escena de felicitación
            SceneManager.LoadScene(nombreEscena);
        }
    }
}
