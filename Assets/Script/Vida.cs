using UnityEngine;

/// <summary>
/// Controla la interacción del objeto de vida con el jugador.
/// Cuando el jugador colisiona, intenta recuperar vida y se destruye si es exitoso.
/// </summary>
public class Vida : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            bool vidaRecuperada = GameManager.Instance.RecuperarVida();
            if (vidaRecuperada)
            {
                Destroy(gameObject);
            }
        }
    }
}
