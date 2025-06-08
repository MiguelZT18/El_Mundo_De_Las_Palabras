using UnityEngine;

/// <summary>
/// Controla el movimiento del jugador, sus ataques y recepción de daño.
/// Permite usar tanto controles físicos (teclado) como botones UI móviles.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>Indica si el jugador ha muerto.</summary>
    public bool muerto;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private Vector2 lastMoveDir;
    private bool Atacando;
    private bool recibiendoDanio;

    [Header("Ataque")]
    public float radioAtaque = 0.6f;
    public int danoAtaque = 1;
    public LayerMask capaEnemigos;
    public Transform puntoAtaque;

    [Header("Vida y daño")]
    public float tiempoInvulnerable = 0.5f;
    public int vida = 3;
    public float fuerzaRebote = 6f;

    // Movimiento
    private float horizontal;
    private float vertical;

    // Controles móviles
    private float btnHorizontal = 0;
    private float btnVertical = 0;

    /// <summary>Inicializa referencias.</summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    /// <summary>Procesa entrada de usuario cada frame.</summary>
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        // Prioriza teclado sobre botones UI
        horizontal = inputX != 0 ? inputX : btnHorizontal;
        vertical = inputY != 0 ? inputY : btnVertical;

        movement = new Vector2(horizontal, vertical);

        if (movement != Vector2.zero)
            lastMoveDir = movement;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetBool("Atacando", Atacando);

        // Voltea sprite horizontalmente
        if (movement.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (movement.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // Ataque con teclado
        if (Input.GetKeyDown(KeyCode.Z))
            Atacar();

        // Acción extra (botón B)
        if (Input.GetKeyDown(KeyCode.Space))
            BotonB();
    }

    /// <summary>Mueve físicamente al jugador.</summary>
    void FixedUpdate()
    {
        if (movement != Vector2.zero)
            rb.MovePosition(rb.position + movement.normalized * 5 * Time.fixedDeltaTime);
    }

    /// <summary>Ejecuta el ataque del jugador.</summary>
    public void Atacar()
    {
        atacando();
        EjecutarAtaque();
    }

    /// <summary>Alias para botón UI A.</summary>
    public void BotonA() => Atacar();

    /// <summary>Llama a evento externo (botón B o barra espaciadora).</summary>
    public void BotonB()
    {
        Debug.Log("Botón B (Espacio) presionado");
        GameEvents.BotonBPress();
    }

    // ----- MÉTODOS PARA BOTONES UI DE MOVIMIENTO -----
    public void TeclaArriba(bool presionado) => btnVertical = presionado ? 1 : (btnVertical == 1 ? 0 : btnVertical);
    public void TeclaAbajo(bool presionado) => btnVertical = presionado ? -1 : (btnVertical == -1 ? 0 : btnVertical);
    public void TeclaIzquierda(bool presionado) => btnHorizontal = presionado ? -1 : (btnHorizontal == -1 ? 0 : btnHorizontal);
    public void TeclaDerecha(bool presionado) => btnHorizontal = presionado ? 1 : (btnHorizontal == 1 ? 0 : btnHorizontal);

    /// <summary>Activa la animación de ataque.</summary>
    public void atacando() => Atacando = true;

    /// <summary>Desactiva la animación de ataque.</summary>
    public void desactivarAtaque() => Atacando = false;

    /// <summary>Detecta enemigos en el rango de ataque y les aplica daño.</summary>
    void EjecutarAtaque()
    {
        Collider2D[] enemigosGolpeados = Physics2D.OverlapCircleAll(puntoAtaque.position, radioAtaque, capaEnemigos);
        foreach (Collider2D enemigo in enemigosGolpeados)
        {
            Enemigo e = enemigo.GetComponent<Enemigo>();
            if (e != null)
                e.RecibirDanio(danoAtaque);
        }
    }

    /// <summary>Dibuja visualmente el radio de ataque en la escena.</summary>
    void OnDrawGizmosSelected()
    {
        if (puntoAtaque != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(puntoAtaque.position, radioAtaque);
        }
    }

    /// <summary>Devuelve si el jugador está atacando.</summary>
    public bool EstaAtacando() => Atacando;

    /// <summary>Corrutina que habilita invulnerabilidad temporal tras recibir daño.</summary>
    private System.Collections.IEnumerator TemporizadorDanio()
    {
        recibiendoDanio = true;
        yield return new WaitForSeconds(tiempoInvulnerable);
        recibiendoDanio = false;
    }

    /// <summary>Recibe daño y activa invulnerabilidad temporal.</summary>
    /// <param name="cantidad">Cantidad de daño recibido.</param>
    public void RecibeDanio(int cantidad)
    {
        if (!recibiendoDanio)
        {
            GameManager.Instance.PerderVida();
            StartCoroutine(TemporizadorDanio());
        }
    }
}
