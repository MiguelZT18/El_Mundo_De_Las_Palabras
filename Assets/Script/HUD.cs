using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Controla la interfaz de usuario (HUD) del juego, mostrando puntos y vidas.
/// </summary>
public class HUD : MonoBehaviour
{
    /// <summary>
    /// Referencia al texto que muestra los puntos.
    /// </summary>
    public TextMeshProUGUI puntos;

    /// <summary>
    /// Arreglo de GameObjects que representan las vidas del jugador.
    /// </summary>
    public GameObject[] vidas;

    /// <summary>
    /// Actualiza el texto de puntos cada cuadro, sincronizándolo con el GameManager.
    /// </summary>
    void Update()
    {
        puntos.text = GameManager.Instance.PuntosTotales.ToString();
    }

    /// <summary>
    /// Actualiza manualmente el texto de puntos en el HUD.
    /// </summary>
    /// <param name="puntosTotales">Puntaje a mostrar.</param>
    public void ActualizarPuntos(int puntosTotales)
    {
        puntos.text = puntosTotales.ToString();
    }

    /// <summary>
    /// Oculta una vida del HUD según el índice.
    /// </summary>
    /// <param name="indice">Índice de la vida a desactivar.</param>
    public void DesactivarVida(int indice)
    {
        vidas[indice].SetActive(false);
    }

    /// <summary>
    /// Muestra una vida del HUD según el índice.
    /// </summary>
    /// <param name="indice">Índice de la vida a activar.</param>
    public void ActivarVida(int indice)
    {
        vidas[indice].SetActive(true);
    }

    /// <summary>
    /// Restaura el HUD con un número específico de vidas activas y puntaje actual.
    /// </summary>
    /// <param name="vidasRestantes">Número de vidas activas a mostrar.</param>
    /// <param name="puntos">Puntaje a mostrar.</param>
    public void ResetearHUD(int vidasRestantes, int puntos)
    {
        for (int i = 0; i < vidas.Length; i++)
        {
            vidas[i].SetActive(i < vidasRestantes);
        }

        ActualizarPuntos(puntos);
    }
}
