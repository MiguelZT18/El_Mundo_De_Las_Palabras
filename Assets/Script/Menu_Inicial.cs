using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

/// <summary>
/// Controlador del menú principal del juego, maneja navegación entre paneles,
/// configuración de audio, pantalla completa y carga de escenas.
/// </summary>
public class MenuController : MonoBehaviour
{
    /// <summary>
    /// Mezclador de audio para ajustar el volumen general.
    /// </summary>
    [SerializeField] private AudioMixer audioMixer;

    [Header("Paneles del Menú")]
    /// <summary>Panel principal del menú.</summary>
    public GameObject panelPrincipal;

    /// <summary>Panel de opciones.</summary>
    public GameObject panelOpciones;

    /// <summary>Panel de créditos.</summary>
    public GameObject panelCreditos;

    /// <summary>
    /// Se llama al iniciar el menú, carga configuraciones previas.
    /// </summary>
    void Start()
    {
        CargarConfiguraciones();
    }

    /// <summary>
    /// Inicia el juego cargando la escena inicial.
    /// </summary>
    public void Jugar()
    {
        Debug.Log("Cargando escena del juego...");
        SceneManager.LoadScene("Intro_Historia");
    }

    /// <summary>
    /// Sale de la aplicación.
    /// </summary>
    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    /// <summary>
    /// Activa o desactiva el modo pantalla completa y guarda la preferencia.
    /// </summary>
    /// <param name="pantallaCompleta">True para pantalla completa, false para modo ventana.</param>
    public void PantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
        PlayerPrefs.SetInt("PantallaCompleta", pantallaCompleta ? 1 : 0);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Cambia el volumen de la mezcla de audio y guarda la preferencia.
    /// </summary>
    /// <param name="volumen">Valor del volumen entre 0.0001 y 1.</param>
    public void cambiarVolumen(float volumen)
    {
        audioMixer.SetFloat("Volumen", Mathf.Log10(volumen) * 20); // Convierte a decibelios
        PlayerPrefs.SetFloat("Volumen", volumen);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Muestra el panel de opciones.
    /// </summary>
    public void IrAOpciones()
    {
        CambiarPanel(panelPrincipal, panelOpciones);
    }

    /// <summary>
    /// Vuelve al menú principal desde las opciones.
    /// </summary>
    public void VolverAlMenuPrincipal()
    {
        CambiarPanel(panelOpciones, panelPrincipal);
    }

    /// <summary>
    /// Muestra el panel de créditos.
    /// </summary>
    public void IrACreditos()
    {
        CambiarPanel(panelPrincipal, panelCreditos);
    }

    /// <summary>
    /// Vuelve al menú principal desde los créditos.
    /// </summary>
    public void VolverDeCreditos()
    {
        CambiarPanel(panelCreditos, panelPrincipal);
    }

    /// <summary>
    /// Activa un panel y desactiva otro.
    /// </summary>
    /// <param name="panelActual">Panel que se desactivará.</param>
    /// <param name="panelDestino">Panel que se activará.</param>
    private void CambiarPanel(GameObject panelActual, GameObject panelDestino)
    {
        if (panelActual != null) panelActual.SetActive(false);
        if (panelDestino != null) panelDestino.SetActive(true);
    }

    /// <summary>
    /// Carga configuraciones previas de volumen y pantalla completa desde PlayerPrefs.
    /// </summary>
    private void CargarConfiguraciones()
    {
        // Volumen
        float volumenGuardado = PlayerPrefs.GetFloat("Volumen", 1f); // Por defecto 100%
        audioMixer.SetFloat("Volumen", Mathf.Log10(volumenGuardado) * 20);

        // Pantalla completa
        bool esPantallaCompleta = PlayerPrefs.GetInt("PantallaCompleta", 1) == 1;
        Screen.fullScreen = esPantallaCompleta;
    }
}
