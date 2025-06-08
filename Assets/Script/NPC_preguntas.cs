using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Controla la lógica de preguntas interactivas que puede realizar un NPC.
/// Se guarda el progreso del jugador usando PlayerPrefs.
/// </summary>
public class NPCPreguntas : MonoBehaviour
{
    /// <summary>
    /// Representa una pregunta con su texto, opciones y la respuesta correcta.
    /// </summary>
    [System.Serializable]
    public class Pregunta
    {
        public string textoPregunta;
        public List<string> opciones;
        public int respuestaCorrecta;
    }

    [Header("Identificación del NPC")]
    [Tooltip("Debe ser único para cada NPC (ej. NPC1, NPCVilla, etc.)")]
    public string npcID = "NPC1";

    [Header("Preguntas y UI")]
    public List<Pregunta> preguntas;
    public GameObject panelPreguntas;
    public TextMeshProUGUI textoPregunta;
    public List<Button> botonesRespuesta;
    public TextMeshProUGUI textoRetroalimentacion;

    private int indicePreguntaActual = 0;
    private bool puedeContestar = false;
    private bool jugadorEnRango = false;

    void Start()
    {
        // Si el jugador ya completó este NPC, se elimina automáticamente
        if (PlayerPrefs.GetInt(npcID + "_resuelto", 0) == 1)
        {
            Destroy(gameObject);
            return;
        }

        panelPreguntas.SetActive(false);
        Time.timeScale = 1f;

        // Asigna funciones a cada botón de respuesta
        for (int i = 0; i < botonesRespuesta.Count; i++)
        {
            int index = i;
            botonesRespuesta[i].onClick.AddListener(() => SeleccionarRespuesta(index));
        }
    }

    void Update()
    {
        // Si el jugador está en rango y presiona espacio, inicia las preguntas
        if (Input.GetKeyDown(KeyCode.Space) && jugadorEnRango && !panelPreguntas.activeSelf)
        {
            Time.timeScale = 0f;
            panelPreguntas.SetActive(true);
            MostrarPregunta();
        }
    }

    /// <summary>
    /// Permite activar las preguntas mediante el botón B (usado por UI o controlador).
    /// </summary>
    public void ActivarPreguntasDesdeBotonB()
    {
        if (jugadorEnRango && !panelPreguntas.activeSelf)
        {
            Time.timeScale = 0f;
            panelPreguntas.SetActive(true);
            MostrarPregunta();
        }
    }

    /// <summary>
    /// Muestra la pregunta actual y actualiza las opciones de respuesta.
    /// </summary>
    void MostrarPregunta()
    {
        puedeContestar = true;
        textoRetroalimentacion.text = "";

        Pregunta p = preguntas[indicePreguntaActual];
        textoPregunta.text = p.textoPregunta;

        for (int i = 0; i < botonesRespuesta.Count; i++)
        {
            if (i < p.opciones.Count)
            {
                botonesRespuesta[i].gameObject.SetActive(true);
                botonesRespuesta[i].GetComponentInChildren<TextMeshProUGUI>().text = p.opciones[i];
                botonesRespuesta[i].interactable = true;
            }
            else
            {
                botonesRespuesta[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Verifica si la respuesta seleccionada es correcta y da retroalimentación.
    /// </summary>
    /// <param name="index">Índice del botón presionado.</param>
    void SeleccionarRespuesta(int index)
    {
        if (!puedeContestar) return;
        puedeContestar = false;

        Pregunta p = preguntas[indicePreguntaActual];

        // Desactiva todos los botones mientras se evalúa
        foreach (var boton in botonesRespuesta)
            boton.interactable = false;

        if (index == p.respuestaCorrecta)
        {
            textoRetroalimentacion.text = "¡Correcto!";
            GameManager.Instance?.SumarPuntos(100);
        }
        else
        {
            textoRetroalimentacion.text = "Incorrecto.";
        }

        indicePreguntaActual++;

        if (indicePreguntaActual < preguntas.Count)
        {
            StartCoroutine(SiguientePreguntaTrasRetraso());
        }
        else
        {
            foreach (var boton in botonesRespuesta)
                boton.gameObject.SetActive(false);

            StartCoroutine(FinalizarPreguntas());
        }
    }

    /// <summary>
    /// Espera un breve tiempo antes de mostrar la siguiente pregunta.
    /// </summary>
    IEnumerator SiguientePreguntaTrasRetraso()
    {
        yield return new WaitForSecondsRealtime(2f);
        MostrarPregunta();
    }

    /// <summary>
    /// Finaliza la secuencia de preguntas, guarda el estado y elimina el NPC.
    /// </summary>
    IEnumerator FinalizarPreguntas()
    {
        textoPregunta.text = "¡Has terminado!";
        textoRetroalimentacion.text = $"Puntaje final: {GameManager.Instance?.PuntosTotales}";

        yield return new WaitForSecondsRealtime(3f);

        PlayerPrefs.SetInt(npcID + "_resuelto", 1);
        PlayerPrefs.Save();

        Destroy(gameObject);
        panelPreguntas.SetActive(false);
        Time.timeScale = 1f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            jugadorEnRango = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            jugadorEnRango = false;
    }

    void OnEnable()
    {
        GameEvents.OnBotonBPressed += ActivarPreguntasDesdeBotonB;
    }

    void OnDisable()
    {
        GameEvents.OnBotonBPressed -= ActivarPreguntasDesdeBotonB;
    }
}
