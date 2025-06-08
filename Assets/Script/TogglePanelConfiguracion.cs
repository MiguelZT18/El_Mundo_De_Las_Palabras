using UnityEngine;

/// <summary>
/// Controla la activación y desactivación del panel de configuración,
/// pausando y reanudando el juego según corresponda.
/// </summary>
public class TogglePanelConfiguracion : MonoBehaviour
{
    /// <summary>
    /// Referencia al panel de configuración que se mostrará u ocultará.
    /// </summary>
    public GameObject panelConfiguracion;

    /// <summary>
    /// Alterna el estado activo del panel de configuración.
    /// Si se activa el panel, pausa el juego (Time.timeScale = 0).
    /// Si se desactiva, reanuda el juego (Time.timeScale = 1).
    /// </summary>
    public void AlternarPanel()
    {
        bool estaActivo = panelConfiguracion.activeSelf;
        panelConfiguracion.SetActive(!estaActivo);

        // Pausar o reanudar el juego según el estado del panel
        Time.timeScale = estaActivo ? 1f : 0f;
    }

    /// <summary>
    /// Cierra el panel de configuración y reanuda el juego.
    /// </summary>
    public void CerrarPanel()
    {
        panelConfiguracion.SetActive(false);
        Time.timeScale = 1f; // Reanudar el juego
    }
}
