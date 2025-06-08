using UnityEngine;

/// <summary>
/// Controla el comportamiento del NPC Final Boss.
/// Al ser derrotado, entrega un objeto de recompensa (como un diccionario) y se destruye.
/// </summary>
public class NPCFinalBoss : MonoBehaviour
{
    [Header("Objeto que se dropea al derrotar")]

    /// <summary>Prefab que se entregará como premio al derrotar al NPC.</summary>
    public GameObject objetoPremio;

    /// <summary>Punto en la escena donde se instanciará el objetoPremio.</summary>
    public Transform puntoSpawn;

    private bool derrotado = false;

    /// <summary>
    /// Ejecuta la secuencia de derrota: entrega el premio, reproduce animación/sonido (si aplica)
    /// y destruye este GameObject.
    /// </summary>
    public void Derrotar()
    {
        if (derrotado) return;
        derrotado = true;

        // 1. Instancia el objeto premio (diccionario)
        if (objetoPremio != null && puntoSpawn != null)
        {
            Instantiate(objetoPremio, puntoSpawn.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Falta asignar el objetoPremio o puntoSpawn en el inspector.");
        }

        // 2. Aquí puedes añadir una animación de muerte o sonido final

        // 3. Destruye a Anton (este GameObject)
        Destroy(gameObject, 0.5f);
    }

    /// <summary>
    /// Método auxiliar para probar la función de derrota desde otros scripts o desde un botón en la UI.
    /// </summary>
    public void SimularVictoria()
    {
        Derrotar();
    }

    /// <summary>
    /// Detecta colisiones con el jugador (opcionalmente activa la derrota).
    /// </summary>
    /// <param name="other">Collider del objeto que colisiona.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !derrotado)
        {
            // Esto es solo si quieres que el jugador lo derrote por colisión
            // Derrotar();
        }
    }
}
