using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Controla la configuración de volumen y pantalla completa en el juego.
/// Permite guardar y cargar preferencias del jugador mediante PlayerPrefs.
/// </summary>
public class ConfiguracionEnJuego : MonoBehaviour
{
    /// <summary>
    /// Mezclador de audio utilizado para ajustar el volumen.
    /// </summary>
    public AudioMixer audioMixer;

    /// <summary>
    /// Control deslizante (Slider) para ajustar el volumen.
    /// </summary>
    public Slider sliderVolumen;

    /// <summary>
    /// Interruptor (Toggle) para activar o desactivar la pantalla completa.
    /// </summary>
    public Toggle togglePantallaCompleta;

    /// <summary>
    /// Inicializa la configuración al inicio del juego.
    /// Carga las preferencias guardadas o establece los valores por defecto si se han reiniciado.
    /// </summary>
    void Start()
    {
        ReiniciarPlayerPrefs(); // Solo para pruebas. ¡Quitar en producción!

        // Cargar las configuraciones guardadas
        float volumen = PlayerPrefs.GetFloat("Volumen", 1f);
        sliderVolumen.value = volumen;
        audioMixer.SetFloat("Volumen", Mathf.Log10(volumen) * 20);

        bool pantallaCompleta = PlayerPrefs.GetInt("PantallaCompleta", 1) == 1;
        togglePantallaCompleta.isOn = pantallaCompleta;
        Screen.fullScreen = pantallaCompleta;

        // Eventos para actualizar en tiempo real
        sliderVolumen.onValueChanged.AddListener(CambiarVolumen);
        togglePantallaCompleta.onValueChanged.AddListener(CambiarPantallaCompleta);
    }

    /// <summary>
    /// Cambia el volumen del audio y guarda el valor en PlayerPrefs.
    /// </summary>
    /// <param name="valor">Valor del volumen entre 0.0001 y 1.0.</param>
    public void CambiarVolumen(float valor)
    {
        audioMixer.SetFloat("Volumen", Mathf.Log10(valor) * 20);
        PlayerPrefs.SetFloat("Volumen", valor);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Activa o desactiva la pantalla completa y guarda la configuración.
    /// </summary>
    /// <param name="valor">True para pantalla completa, false para ventana.</param>
    public void CambiarPantallaCompleta(bool valor)
    {
        Screen.fullScreen = valor;
        PlayerPrefs.SetInt("PantallaCompleta", valor ? 1 : 0);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Elimina todas las configuraciones guardadas y establece valores por defecto.
    /// Ideal para pruebas o botón de "restablecer configuración".
    /// </summary>
    public void ReiniciarPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();

        // Establecer valores por defecto
        PlayerPrefs.SetFloat("Volumen", 0.5f);
        PlayerPrefs.SetInt("PantallaCompleta", 1);
        PlayerPrefs.SetInt("Puntaje", 0);
        PlayerPrefs.Save();

        Debug.Log("PlayerPrefs reiniciados y valores por defecto establecidos.");
    }
}
