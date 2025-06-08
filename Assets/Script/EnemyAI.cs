using UnityEngine;

/// <summary>
/// Controla la IA del enemigo para que persiga al jugador si está dentro del rango de detección,
/// y regrese a su posición inicial si el jugador se aleja.
/// </summary>
public class EnemyAI : MonoBehaviour
{
    /// <summary>
    /// Velocidad de movimiento del enemigo.
    /// </summary>
    public float speed = 2f;

    /// <summary>
    /// Rango de detección del jugador.
    /// </summary>
    public float detectionRange = 5f;

    private Transform player;
    private Vector3 startPosition;
    private Rigidbody2D rb;
    private Animator animator;

    /// <summary>
    /// Referencia al componente <see cref="Enemigo"/> para verificar si está muerto.
    /// </summary>
    private Enemigo enemigo;

    /// <summary>
    /// Inicializa las referencias necesarias del enemigo.
    /// </summary>
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemigo = GetComponent<Enemigo>();
    }

    /// <summary>
    /// Lógica de movimiento del enemigo hacia el jugador o hacia su posición inicial.
    /// </summary>
    void Update()
    {
        if (enemigo != null && enemigo.EstaMuerto())
        {
            StopMovement();
            return;
        }

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            MoveTowards(player.position);
        }
        else if (Vector3.Distance(transform.position, startPosition) > 0.1f)
        {
            MoveTowards(startPosition);
        }
        else
        {
            StopMovement();
        }
    }

    /// <summary>
    /// Mueve al enemigo hacia una posición objetivo.
    /// </summary>
    /// <param name="targetPosition">Posición hacia la que se moverá el enemigo.</param>
    void MoveTowards(Vector3 targetPosition)
    {
        Vector2 direction = (targetPosition - transform.position).normalized;

        if (direction.magnitude > 0.05f)
        {
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveY", direction.y);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    /// <summary>
    /// Detiene la animación de movimiento del enemigo.
    /// </summary>
    void StopMovement()
    {
        animator.SetBool("isMoving", false);
    }

    /// <summary>
    /// Dibuja en el editor el rango de detección del enemigo.
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
