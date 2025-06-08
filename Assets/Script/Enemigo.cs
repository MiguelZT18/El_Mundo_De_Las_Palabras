using UnityEngine;

/// <summary>
/// Controla la salud y muerte del enemigo, así como la interacción con el GameManager.
/// </summary>
public class Enemigo : MonoBehaviour
{
    /// <summary>
    /// Cantidad de vida del enemigo.
    /// </summary>
    public int vida = 3;

    /// <summary>
    /// Indica si el enemigo ya ha muerto.
    /// </summary>
    public bool muerto = false;

    private Animator animator;

    /// <summary>
    /// Inicializa el componente Animator al comenzar el juego.
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Aplica daño al enemigo y gestiona su muerte si la vida llega a 0 o menos.
    /// </summary>
    /// <param name="cantidad">Cantidad de daño recibido.</param>
    public void RecibirDanio(int cantidad)
    {
        if (muerto) return;

        vida -= cantidad;

        if (vida <= 0)
        {
            muerto = true;
            animator.SetTrigger("Morir");
            GetComponent<Collider2D>().enabled = false;

            // Sumar puntos al jugador
            GameManager.Instance?.SumarPuntos(100);

            // Recuperar una vida si aplica
            GameManager.Instance?.RecuperarVida();

            // Si es un jefe final, llama su función de derrota
            NPCFinalBoss jefeFinal = GetComponent<NPCFinalBoss>();
            if (jefeFinal != null)
            {
                jefeFinal.Derrotar();
                return;
            }

            // Destruir el enemigo después de un tiempo
            Destroy(gameObject, 1.5f);
        }
    }

    /// <summary>
    /// Verifica si el enemigo ha muerto.
    /// </summary>
    /// <returns>True si el enemigo está muerto, de lo contrario False.</returns>
    public bool EstaMuerto()
    {
        return muerto;
    }
}
