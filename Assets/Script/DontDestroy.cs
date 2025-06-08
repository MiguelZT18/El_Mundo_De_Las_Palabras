using UnityEngine;

/// <summary>
/// Se asegura de que el objeto del jugador (u otro GameObject importante) persista entre escenas.
/// Previene la creación de duplicados.
/// </summary>
public class PlayerPersistence : MonoBehaviour
{
    /// <summary>
    /// Instancia estática única de este script para implementar el patrón Singleton.
    /// </summary>
    private static PlayerPersistence instance;

    /// <summary>
    /// Método que se llama cuando el objeto se instancia.
    /// Verifica si ya existe una instancia y, si no, la asigna y marca como persistente entre escenas.
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre escenas
        }
        else
        {
            Destroy(gameObject); // Evita duplicados si ya existe una instancia
        }
    }
}
