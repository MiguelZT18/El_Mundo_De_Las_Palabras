using UnityEngine;

/// <summary>
/// Controla el comportamiento de ataque del enemigo cuando el jugador está en rango.
/// </summary>
public class EnemigoAtaque : MonoBehaviour
{
    /// <summary>
    /// Daño que inflige el enemigo al jugador por cada ataque.
    /// </summary>
    public int danio = 1;

    /// <summary>
    /// Tiempo de espera entre ataques consecutivos.
    /// </summary>
    public float tiempoEntreAtaques = 1f;

    /// <summary>
    /// Tiempo en el que el enemigo podrá volver a atacar.
    /// </summary>
    private float tiempoProximoAtaque = 0f;

    private Animator animator;
    private Transform jugador;
    private bool jugadorEnRango = false;

    private Enemigo enemigo;

    /// <summary>
    /// Inicializa referencias necesarias al comenzar el juego.
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
        enemigo = GetComponent<Enemigo>();
    }

    /// <summary>
    /// Revisa si el jugador está en rango y si es momento de atacar.
    /// </summary>
    void Update()
    {
        // Detener comportamiento si el enemigo ha muerto
        if (enemigo != null && enemigo.EstaMuerto()) return;

        if (jugadorEnRango && Time.time >= tiempoProximoAtaque)
        {
            tiempoProximoAtaque = Time.time + tiempoEntreAtaques;
            AtacarJugador();
        }
        else
        {
            animator.SetBool("Atacando", false);
        }
    }

    /// <summary>
    /// Ejecuta el ataque al jugador, reproduciendo la animación y aplicando daño.
    /// </summary>
    void AtacarJugador()
    {
        animator.SetBool("Atacando", true);

        if (jugador != null)
        {
            PlayerMovement pm = jugador.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.RecibeDanio(danio);
            }
        }
    }

    /// <summary>
    /// Detecta cuándo el jugador entra en el área de ataque.
    /// </summary>
    /// <param name="other">Collider del objeto que entra.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = true;
            jugador = other.transform;
        }
    }

    /// <summary>
    /// Detecta cuándo el jugador sale del área de ataque.
    /// </summary>
    /// <param name="other">Collider del objeto que sale.</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = false;
            jugador = null;
        }
    }
}
