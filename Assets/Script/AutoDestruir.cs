using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Este script impide que el objeto se destruya al cambiar de escena, 
/// pero lo destruye automáticamente cuando se carga una escena específica.
/// </summary>
public class AutoDestruirEnEscena : MonoBehaviour
{
    /// <summary>
    /// Nombre de la escena en la que este objeto debe destruirse automáticamente.
    /// </summary>
    [SerializeField] private string escenaDestruir = "EscenaFelicitacion"; // Ajusta al nombre real

    /// <summary>
    /// Evita que el objeto se destruya al cambiar de escena.
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Se suscribe al evento de escena cargada cuando el objeto se habilita.
    /// </summary>
    void OnEnable()
    {
        SceneManager.sceneLoaded += RevisarEscena;
    }

    /// <summary>
    /// Se desuscribe del evento de escena cargada cuando el objeto se deshabilita.
    /// </summary>
    void OnDisable()
    {
        SceneManager.sceneLoaded -= RevisarEscena;
    }

    /// <summary>
    /// Revisa si la escena recién cargada es la escena objetivo para destruir este objeto.
    /// </summary>
    /// <param name="escena">La escena que se ha cargado.</param>
    /// <param name="modo">El modo en que la escena fue cargada.</param>
    void RevisarEscena(Scene escena, LoadSceneMode modo)
    {
        if (escena.name == escenaDestruir)
        {
            Destroy(gameObject);
        }
    }
}
