using TMPro;
using UnityEngine;

public class UIGanancias : MonoBehaviour
{
    [Header("Textos")]
    public TextMeshProUGUI textoGananciasDia;
    public TextMeshProUGUI textoPendiente;
    public TextMeshProUGUI textoAhorroTotal;
    public TextMeshProUGUI textoDia;
    [Header("Solo completar en el Acto 1 (dejar vacío en el Acto 2)")]
    public GeneradorClientes generadorClientes;
    private void OnEnable()
    {
        GestorGanancias.OnValoresActualizados += ActualizarUI;
        ActualizarUI();
    }
    private void OnDisable()
    {
        GestorGanancias.OnValoresActualizados -= ActualizarUI;
    }
    private void Update()
    {
        // La fila cambia todo el tiempo, así que esta sí se refresca cada frame
        if (generadorClientes != null && textoPendiente != null)
        {
            textoPendiente.text = "Por cobrar: $" + generadorClientes.ObtenerGananciaPendienteEnFila();
        }
    }
    void ActualizarUI()
    {
        if (GestorGanancias.Instancia == null) return;
        if (textoGananciasDia!=null) textoGananciasDia.text = "Hoy: $" + GestorGanancias.Instancia.gananciasDelDia;
        if (textoAhorroTotal != null) textoAhorroTotal.text = "Ahorro total: $" + GestorGanancias.Instancia.ahorroTotal;
        if (textoDia != null) textoDia.text = "Día " + GestorGanancias.Instancia.diasActuales + " - " + GestorGanancias.Instancia.ObtenerFechaFormateada();
    }
}
