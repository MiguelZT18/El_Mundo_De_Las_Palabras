using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// Gestiona el estado global del juego, incluyendo vidas, puntos y persistencia entre escenas.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Instancia única (singleton) del GameManager.
    /// </summary>
    public static GameManager Instance { get; private set; }

    /// <summary>
    /// Referencia al HUD para actualizar la interfaz del jugador.
    /// </summary>
    public HUD hud;

    /// <summary>
    /// Puntos acumulados durante el juego.
    /// </summary>
    public int PuntosTotales { get; private set; }

    private int vidas = 3;

    /// <summary>
    /// Inicializa el singleton y mantiene el objeto entre escenas.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            // Evitar que esté como hijo accidentalmente
            if (transform.parent != null)
                transform.SetParent(null);

            // Evita que se destruya al cambiar de escena
            DontDestroyOnLoad(gameObject);

            ReiniciarEstado();
        }
        else
        {
            Destroy(gameObject); // Evita duplicados
        }
    }

    /// <summary>
    /// Se suscribe al evento de carga de escena para reinicializar el HUD.
    /// </summary>
    [System.Obsolete]
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// Cancela la suscripción al evento de carga de escena.
    /// </summary>
    [System.Obsolete]
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// Reinicializa la referencia al HUD y actualiza sus datos.
    /// </summary>
    /// <param name="scene">La escena recién cargada.</param>
    /// <param name="mode">El modo de carga de escena.</param>
    [System.Obsolete]
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        hud = FindObjectOfType<HUD>();

        if (hud != null)
            hud.ResetearHUD(vidas, PuntosTotales);
    }

    /// <summary>
    /// Reinicia las vidas y puntos a sus valores iniciales.
    /// </summary>
    private void ReiniciarEstado()
    {
        vidas = 3;
        PuntosTotales = 0;
    }

    /// <summary>
    /// Aumenta el puntaje total y actualiza el HUD.
    /// </summary>
    /// <param name="puntosASumar">Cantidad de puntos a añadir.</param>
    public void SumarPuntos(int puntosASumar)
    {
        PuntosTotales += puntosASumar;

        if (hud != null)
            hud.ActualizarPuntos(PuntosTotales);
    }

    /// <summary>
    /// Resta una vida al jugador. Si llega a cero, reinicia el estado y recarga la escena.
    /// </summary>
    public void PerderVida()
    {
        vidas--;

        if (vidas <= 0)
        {
            ReiniciarEstado();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        if (hud != null)
            hud.DesactivarVida(vidas);
    }

    /// <summary>
    /// Intenta recuperar una vida. Si ya tiene el máximo (3), no hace nada.
    /// </summary>
    /// <returns>True si se recuperó una vida, false si ya tenía el máximo.</returns>
    public bool RecuperarVida()
    {
        if (vidas >= 3)
            return false;

        if (hud != null)
            hud.ActivarVida(vidas);

        vidas++;
        return true;
    }
}
