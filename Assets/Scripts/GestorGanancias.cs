using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GestorGanancias : MonoBehaviour
{
    public static GestorGanancias Instancia;
    [Header("Ganancias del día")]
    public int gananciasDelDia = 0;
    [Header("Ahorro total(entre días)")]
    public int ahorroTotal = 0;
    [Header("Reparto")]
    [Range(0f, 1f)]
    public float porcentajeJugador = 0.5f; // % de lo ganado hoy que pasa al ahorro
    [Header("Robos")]
    public int vecesRobado = 0;
    public int maxRobados = 2;
    [Header("Días")]
    public int diasActuales = 1;
    public int diaInicial = 1;
    public int mesInicial = 3;
    public int anioInicial = 2000;
    // Otros scripts (la UI) se suscriben a esto en vez de consultar cada frame
    public static event Action OnValoresActualizados;
    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AgregarGanancia(int monto)
    {
        gananciasDelDia += monto;
        OnValoresActualizados?.Invoke();
    }
    public void Robar()
    {
        vecesRobado++;
        Debug.Log("¡Te robaron! Robos: " + vecesRobado + "/" + maxRobados);
        OnValoresActualizados?.Invoke();
        if (vecesRobado >= maxRobados)
        {
            PerderDia();
        }
    }
    void PerderDia()
    {
        Debug.Log("Perdiste el día. No se agrega nada al ahorro.");
        gananciasDelDia = 0;
        vecesRobado = 0;
        AvanzarDia();
        SceneManager.LoadScene("Acto1");
    }
    public void TerinarDia()
    {
        int montoQueSeLleva = Mathf.RoundToInt(gananciasDelDia * porcentajeJugador);
        ahorroTotal += montoQueSeLleva;

        Debug.Log($"Día terminado. Ganí: {gananciasDelDia}, se lleva: {montoQueSeLleva}");

        gananciasDelDia = 0;
        vecesRobado = 0;
        AvanzarDia();
        SceneManager.LoadScene("Acto1");
    }
    void AvanzarDia()
    {
        diasActuales++;
        OnValoresActualizados?.Invoke();
    }
    public string ObtenerFechaFormateada()
    {
        DateTime fecha = new DateTime(anioInicial, mesInicial, diaInicial).AddDays(diasActuales - 1);
        return fecha.ToString("dd/MM/yyyy");
    }
}
