using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    void Start()
    {
        float x = PlayerPrefs.GetFloat("SpawnX", 0);
        float y = PlayerPrefs.GetFloat("SpawnY", 0);

        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            jugador.transform.position = new Vector2(x, y);
        }
    }
}
