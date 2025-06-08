using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Controla la introducción tipo Final Fantasy, mostrando párrafos con efecto de escritura y cambiando de escena al final.
/// </summary>
public class IntroTextoFinalFantasy : MonoBehaviour
{
    /// <summary>
    /// Lista de párrafos a mostrar uno por uno con efecto de escritura.
    /// </summary>
    [TextArea(5, 15)]
    public string[] parrafos;

    /// <summary>
    /// Texto en pantalla donde se mostrarán los párrafos.
    /// </summary>
    public TextMeshProUGUI textoPantalla;

    /// <summary>
    /// Velocidad con la que se escriben los caracteres (en segundos).
    /// </summary>
    public float velocidadEscritura = 0.05f;

    /// <summary>
    /// Tiempo de pausa entre cada párrafo.
    /// </summary>
    public float pausaEntreParrafos = 1.5f;

    /// <summary>
    /// Nombre de la escena a cargar después de mostrar todos los párrafos.
    /// </summary>
    public string siguienteEscena = "Villa1";

    /// <summary>
    /// Inicia la rutina de mostrar la introducción.
    /// </summary>
    private void Start()
    {
        StartCoroutine(MostrarIntro());
    }

    /// <summary>
    /// Corrutina que muestra cada párrafo con efecto de escritura tipo máquina de escribir.
    /// Al finalizar, espera unos segundos y cambia de escena.
    /// </summary>
    IEnumerator MostrarIntro()
    {
        textoPantalla.text = "";

        foreach (string parrafo in parrafos)
        {
            textoPantalla.text = "";
            foreach (char letra in parrafo)
            {
                textoPantalla.text += letra;
                yield return new WaitForSeconds(velocidadEscritura);
            }

            yield return new WaitForSeconds(pausaEntreParrafos);
        }

        yield return new WaitForSeconds(3f); // Pausa final antes del cambio de escena

        SceneManager.LoadScene(siguienteEscena);
    }
}
