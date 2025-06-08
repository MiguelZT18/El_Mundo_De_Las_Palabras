using System;

/// <summary>
/// Clase estática que define eventos globales del juego.
/// Permite que otros scripts se suscriban y respondan cuando se presionan ciertos botones.
/// </summary>
public static class GameEvents
{
    /// <summary>
    /// Evento que se dispara cuando se presiona el botón B.
    /// </summary>
    public static event Action OnBotonBPressed;

    /// <summary>
    /// Método que invoca el evento <see cref="OnBotonBPressed"/>.
    /// Debe llamarse cuando se detecte la acción correspondiente (presionar botón B).
    /// </summary>
    public static void BotonBPress()
    {
        OnBotonBPressed?.Invoke();
    }
}
